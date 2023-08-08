using AutoFixture;
using FluentAssertions;
using Moq;
using TemplateNetCore.Application.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Commands.v1.Auth.SignIn;
using TemplateNetCore.Domain.Entities.v1;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql;
using TemplateNetCore.Domain.Interfaces.Repositories.Sql.v1;
using TemplateNetCore.Domain.Interfaces.Services.v1;
using TemplateNetCore.Tests.Shared;
using Xunit;

namespace TemplateNetCore.Tests.Commands.v1.Auth.SignIn
{
    public class SignInCommandHandlerTest : BaseTest<SignInCommandHandler>
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IUnityOfWork> _unityOfWorkMock = new();
        private readonly Mock<IHashService> _hashServiceMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();

        public SignInCommandHandlerTest() =>
            SetupDefaultMocks();

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

        protected override SignInCommandHandler MakeSut()
        {
            return new SignInCommandHandler(
                _loggerMock.Object,
                _notificationContextServiceMock.Object,
                _unityOfWorkMock.Object,
                _hashServiceMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Fact(DisplayName = "Should call IUnityOfWork to get user by email")]
        public async Task ShouldCallIUnityOfWorkToGetUserByEmail()
        {
            var command = _fixture.Create<SignInCommand>();

            await MakeSut().Handle(command, CancellationToken.None);

            _userRepositoryMock.Verify(mock => mock.GetByEmailAsync(command.Email), Times.Once);
        }

        [Fact(DisplayName = "Should call IHashService to validate user password")]
        public async Task ShouldCallIHashServiceToValidateUserPassword()
        {
            var command = _fixture.Create<SignInCommand>();
            var user = MakeFakeUser();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            await MakeSut().Handle(command, CancellationToken.None);

            _hashServiceMock.Verify(mock => mock.Compare(user.Password, command.Password), Times.Once);
        }

        [Fact(DisplayName = "Should call ITokenService to generate access token")]
        public async Task ShouldCallITokenServiceToGenerateAccessToken()
        {
            var command = _fixture.Create<SignInCommand>();
            var user = MakeFakeUser();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            await MakeSut().Handle(command, CancellationToken.None);

            _tokenServiceMock.Verify(mock => mock.Generate(user), Times.Once);
        }

        [Fact(DisplayName = "Should returns SignInResponse on success")]
        public async Task ShouldReturnsSignInResponseOnSuccess()
        {
            var command = _fixture.Create<SignInCommand>();
            var accessToken = _fixture.Create<string>();

            _tokenServiceMock
                .Setup(mock => mock.Generate(It.IsAny<User>()))
                .Returns(accessToken);

            var result = await MakeSut().Handle(command, CancellationToken.None);

            result.Should().BeEquivalentTo(new SignInCommandResponse(accessToken, result.RefreshToken));
        }

        [Fact(DisplayName = "Should add INVALID_CREDENTIALS if user not exists")]
        public async Task ShouldAddNotificationIfUserNotExists()
        {
            var command = _fixture.Create<SignInCommand>();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            await MakeSut().Handle(command, CancellationToken.None);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("INVALID_CREDENTIALS"), Times.Once);
        }

        [Fact(DisplayName = "Should add INVALID_CREDENTIALS if user password is invalid")]
        public async Task ShouldAddNotificationIfUserPasswordIsInvalid()
        {
            var command = _fixture.Create<SignInCommand>();

            _hashServiceMock
                .Setup(mock => mock.Compare(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            await MakeSut().Handle(command, CancellationToken.None);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("INVALID_CREDENTIALS"), Times.Once);
        }

        [Fact(DisplayName = "Should add USER_NOT_ACTIVE if user is not active")]
        public async Task ShouldAddNotificationIfUserIsNotActive()
        {
            var command = _fixture.Create<SignInCommand>();
            var user = MakeFakeUser(active: false);

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            await MakeSut().Handle(command, CancellationToken.None);

            _notificationContextServiceMock.Verify(mock => mock.AddNotification("USER_NOT_ACTIVE"), Times.Once);
        }

        [Fact(DisplayName = "Should throw if any dependency throws")]
        public async Task ShouldThrowIfAnyDependencyThrows()
        {
            var command = _fixture.Create<SignInCommand>();

            _userRepositoryMock
                .Setup(mock => mock.GetByEmailAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("An error occurred"));

            Func<Task> act = async () => await MakeSut().Handle(command, CancellationToken.None);

            await act.Should().ThrowExactlyAsync<Exception>()
                .WithMessage("An error occurred");
        }

        private User MakeFakeUser(bool active = true)
        {
            return _fixture.Build<User>()
                .With(dest => dest.Active, active)
                .Create();
        }
    }
}
