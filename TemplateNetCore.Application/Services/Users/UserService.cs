using AutoMapper;
using Microsoft.AspNetCore.Http;
using TemplateNetCore.Application.Exceptions;
using TemplateNetCore.Domain.Commands.SignIn;
using TemplateNetCore.Domain.Commands.SignUp;
using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Domain.Models;
using TemplateNetCore.Repository;

namespace TemplateNetCore.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public UserService(IUnityOfWork unityOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHashService hashService, ITokenService tokenService)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<User> SignUpAsync(SignUpCommand command)
        {
            var validator = new Validator<SignUpCommand, SignUpCommandValidator>(command);

            if (!validator.IsValid())
                throw new ValidationException(validator.GetFirstError());

            var isEmailInUse = await _unityOfWork.UserRepository.AnyAsync(user => user.Email == command.Email);

            if (isEmailInUse)
                throw new BusinessRuleException($"Email {command.Email} is already in use");

            var user = _mapper.Map<User>(command);
            var passwordHash = _hashService.Hash(command.Password);

            user.Create(passwordHash);

            await _unityOfWork.UserRepository.AddAsync(user);
            await _unityOfWork.CommitAsync();

            return user;
        }

        public async Task<string> SignInAsync(SignInCommand command)
        {
            var validator = new Validator<SignInCommand, SignInCommandValidator>(command);

            if (!validator.IsValid())
                throw new ValidationException(validator.GetFirstError());

            var user = await _unityOfWork.UserRepository.GetAsync(u => u.Email == command.Email);

            if (user == null)
                throw new BusinessRuleException("Email not found");

            var isValidPassword = _hashService.Compare(user.Password, command.Password);

            if (!isValidPassword)
                throw new BusinessRuleException("Invalid password");

            if (!user.IsActive)
                throw new BusinessRuleException("User blocked");

            var accessToken = _tokenService.Generate(user);

            return accessToken;
        }

        public async Task<User> GetLoggedUserAsync()
        {
            var loggedUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            var user = await _unityOfWork.UserRepository.GetByIdAsync(loggedUserId);

            return user ?? throw new NotFoundException("User not found");
        }
    }
}
