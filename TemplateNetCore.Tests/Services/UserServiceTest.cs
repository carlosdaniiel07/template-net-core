using AutoFixture;
using Moq;
using Shouldly;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateNetCore.Domain.Dto.Users;
using TemplateNetCore.Domain.Entities.Users;
using TemplateNetCore.Domain.Interfaces.Users;
using TemplateNetCore.Repository;
using TemplateNetCore.Repository.Interfaces.Users;
using TemplateNetCore.Service.Exceptions;
using TemplateNetCore.Service.Users;
using Xunit;

namespace TemplateNetCore.Tests.Services
{
    public class UserServiceTest : BaseTest
    {
        [Fact(DisplayName = "Dado um usuário válido, deve cadastrar com sucesso")]
        public async Task Dado_Um_Usuario_Valido_Deve_Cadastrar_Com_Sucesso()
        {
            // Arrange
            var postSignUpDto = _fixture.Create<PostSignUpDto>();
            var hashedPassword = _fixture.Create<string>();

            var unityOfWorkMock = new Mock<IUnityOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var hashServiceMock = new Mock<IHashService>();

            userRepositoryMock
                .Setup(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);
            hashServiceMock
                .Setup(m => m.Hash(It.IsAny<string>()))
                .Returns(hashedPassword);
            userRepositoryMock
                .Setup(m => m.AddAsync(It.IsAny<User>()));
            unityOfWorkMock
                .Setup(m => m.CommitAsync());
            unityOfWorkMock
                .Setup(m => m.UserRepository)
                .Returns(userRepositoryMock.Object);

            var userService = new UserService(unityOfWorkMock.Object, _mapper, null, hashServiceMock.Object, null);

            // Act
            var user = await userService.SignUp(postSignUpDto);

            // Assert
            userRepositoryMock
                .Verify(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            hashServiceMock
                .Verify(m => m.Hash(It.IsAny<string>()), Times.Once);
            userRepositoryMock
                .Verify(m => m.AddAsync(It.IsAny<User>()), Times.Once);
            unityOfWorkMock
                .Verify(m => m.CommitAsync(), Times.Once);

            user.Name.ShouldBe(postSignUpDto.Name);
            user.Email.ShouldBe(postSignUpDto.Email);
            user.Password.ShouldBe(hashedPassword);
            user.Role.ShouldBe(postSignUpDto.Role);
            user.LastLogin.ShouldBeNull();
            user.IsActive.ShouldBeTrue();
            user.CreatedAt.ShouldNotBe(default);
            user.DeletedAt.ShouldBeNull();
        }

        [Fact(DisplayName = "Dado um usuário inválido (com e-mail já utilizado), deve gerar um erro ao tentar realizar o cadastro")]
        public async Task Dado_Um_Usuario_Com_Email_Ja_Utilizado_Deve_Gerar_Erro_Ao_Cadastrar()
        {
            // Arrange
            var postSignUpDto = _fixture.Create<PostSignUpDto>();
            var expectedException = new BusinessRuleException("Já existe um usuário com este e-mail");

            var unityOfWorkMock = new Mock<IUnityOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);
            unityOfWorkMock
                .Setup(m => m.UserRepository)
                .Returns(userRepositoryMock.Object);

            var userService = new UserService(unityOfWorkMock.Object, _mapper, null, null, null);

            // Act
            var signUp = userService.SignUp(postSignUpDto);

            // Assert
            var exception = await Should.ThrowAsync<BusinessRuleException>(signUp);

            exception.StatusCode.ShouldBe(expectedException.StatusCode);
            exception.Message.ShouldBe(expectedException.Message);

            userRepositoryMock
                .Verify(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            userRepositoryMock
                .Verify(m => m.AddAsync(It.IsAny<User>()), Times.Never);
            unityOfWorkMock
                .Verify(m => m.CommitAsync(), Times.Never);
        }
    }
}
