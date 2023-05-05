using AutoFixture;
using FluentAssertions;
using Moq;
using TemplateNetCore.Application.UseCases.v1.Auth.SignIn;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql.v1;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Domain.UseCases.v1.Auth.SignIn;
using Xunit;

namespace TemplateNetCore.Tests.UseCases.v1.Auth.SignIn
{
    public class SignInUseCaseTest : BaseUseCaseTest<SignInUseCase>
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IUnityOfWork> _unityOfWorkMock = new();
        private readonly Mock<IHashService> _hashServiceMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();

        public SignInUseCaseTest()
        {
            SetupDefaultMocks();
        }
        protected override void SetupDefaultMocks()
        {
            _userRepositoryMock.Reset();
            _unityOfWorkMock.Reset();
            _hashServiceMock.Reset();
            _tokenServiceMock.Reset();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(MakeFakeUser());
            _unityOfWorkMock
                .Setup(mock => mock.UserRepository)
                .Returns(_userRepositoryMock.Object);
            _hashServiceMock
                .Setup(mock => mock.Compare(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _tokenServiceMock
                .Setup(mock => mock.Generate(It.IsAny<User>()))
                .Returns(_fixture.Create<string>());
        }

        [Fact(DisplayName = "Should call IUnityOfWork to get user by email")]
        public async Task ShouldCallIUnityOfWorkToGetUserByEmail()
        {
            var request = _fixture.Create<SignInRequest>();

            await MakeSut().ExecuteAsync(request);

            _userRepositoryMock.Verify(mock => mock.GetByEmailAsync(request.Email), Times.Once);
        }

        [Fact(DisplayName = "Should call IHashService to validate user password")]
        public async Task ShouldCallIHashServiceToValidateUserPassword()
        {
            var request = _fixture.Create<SignInRequest>();
            var user = MakeFakeUser();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            await MakeSut().ExecuteAsync(request);

            _hashServiceMock.Verify(mock => mock.Compare(user.Password, request.Password), Times.Once);
        }

        [Fact(DisplayName = "Should call ITokenService to generate access token")]
        public async Task ShouldCallITokenServiceToGenerateAccessToken()
        {
            var request = _fixture.Create<SignInRequest>();
            var user = MakeFakeUser();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            await MakeSut().ExecuteAsync(request);

            _tokenServiceMock.Verify(mock => mock.Generate(user), Times.Once);
        }

        [Fact(DisplayName = "Should returns SignInResponse on success")]
        public async Task ShouldReturnsSignInResponseOnSuccess()
        {
            var request = _fixture.Create<SignInRequest>();
            var accessToken = _fixture.Create<string>();

            _tokenServiceMock
                .Setup(mock => mock.Generate(It.IsAny<User>()))
                .Returns(accessToken);

            var result = await MakeSut().ExecuteAsync(request);

            result.Should().BeEquivalentTo(new SignInResponse(accessToken, result.RefreshToken));
        }

        [Fact(DisplayName = "Should add INVALID_CREDENTIALS if user not exists")]
        public async Task ShouldAddNotificationIfUserNotExists()
        {
            var request = _fixture.Create<SignInRequest>();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            await MakeSut().ExecuteAsync(request);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("INVALID_CREDENTIALS"), Times.Once);
        }

        [Fact(DisplayName = "Should add INVALID_CREDENTIALS if user password is invalid")]
        public async Task ShouldAddNotificationIfUserPasswordIsInvalid()
        {
            var request = _fixture.Create<SignInRequest>();

            _hashServiceMock
                .Setup(mock => mock.Compare(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            await MakeSut().ExecuteAsync(request);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("INVALID_CREDENTIALS"), Times.Once);
        }

        [Fact(DisplayName = "Should add USER_NOT_ACTIVE if user is not active")]
        public async Task ShouldAddNotificationIfUserIsNotActive()
        {
            var request = _fixture.Create<SignInRequest>();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(MakeFakeUser(active: false));

            await MakeSut().ExecuteAsync(request);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("USER_NOT_ACTIVE"), Times.Once);
        }

        [Fact(DisplayName = "Should throw if any dependency throws")]
        public async Task ShouldThrowIfAnyDependencyThrows()
        {
            var request = _fixture.Create<SignInRequest>();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("An error occurred"));

            Func<Task> act = async () => await MakeSut().ExecuteAsync(request);

            await act.Should().ThrowExactlyAsync<Exception>()
                .WithMessage("An error occurred");
        }

        protected override SignInUseCase MakeSut()
        {
            return new SignInUseCase(
                _loggerMock.Object,
                _notificationContextServiceMock.Object,
                _unityOfWorkMock.Object,
                _hashServiceMock.Object,
                _tokenServiceMock.Object
            );
        }

        private User MakeFakeUser(bool active = true)
        {
            return _fixture.Build<User>()
                .With(dest => dest.Active, active)
                .Create();
        }
    }
}
