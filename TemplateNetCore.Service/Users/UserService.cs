using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Repository;
using TemplateNetCore.Service.Exceptions;

namespace TemplateNetCore.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public UserService(IUnityOfWork unityOfWork, IHashService hashService, ITokenService tokenService)
        {
            _unityOfWork = unityOfWork;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _unityOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            return user;
        }

        public Guid GetLoggedUserId(ClaimsPrincipal claims)
        {
            return _tokenService.GetIdByClaims(claims);
        }

        public async Task<GetLoginResponseDto> Login(PostLoginDto postLoginDto)
        {
            var user = await _unityOfWork.UserRepository.GetAsync(user => user.Email == postLoginDto.Email);
            var isInvalidPassword = user == null || !_hashService.Compare(user.Password, postLoginDto.Password);

            if (isInvalidPassword)
            {
                throw new NotFoundException("Usuário e/ou senha incorreta");
            }

            return new GetLoginResponseDto
            {
                AccessToken = _tokenService.Generate(user)
            };
        }

        public async Task SignUp(User user)
        {
            var emailExists = await _unityOfWork.UserRepository.AnyAsync(user => user.Email == user.Email);

            if (emailExists)
            {
                throw new BusinessRuleException("Já existe um usuário com este e-mail");
            }

            user.Password = _hashService.Hash(user.Password);
            user.LastLogin = null;
            user.IsActive = true;

            await _unityOfWork.UserRepository.AddAsync(user);
            await _unityOfWork.CommitAsync();
        }
    }
}
