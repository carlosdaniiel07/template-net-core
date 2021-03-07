using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Domain.Dto.Users;
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

        public GetLoginResponseDto Login(PostLoginDto postLoginDto)
        {
            var user = _unityOfWork.UserRepository.Get(user => user.Email == postLoginDto.Email);
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

        public void SignUp(PostSignUpDto signUpDto)
        {
            var emailExists = _unityOfWork.UserRepository.Any(user => user.Email == signUpDto.Email);

            if (emailExists)
            {
                throw new BusinessRuleException("Já existe um usuário com este e-mail");
            }

            var user = new User
            {
                Name = signUpDto.Name,
                Email = signUpDto.Email,
                Password = _hashService.Hash(signUpDto.Password),
                Role = signUpDto.Role,
                LastLogin = null,
                IsActive = true,
            };

            _unityOfWork.UserRepository.Add(user);
            _unityOfWork.Commit();
        }
    }
}
