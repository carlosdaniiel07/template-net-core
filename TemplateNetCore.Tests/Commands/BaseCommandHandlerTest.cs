using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TemplateNetCore.Application.Commands.v1.Auth.SignUp;
using TemplateNetCore.Domain.Interfaces.Services.v1;

namespace TemplateNetCore.Tests.Commands
{
    public abstract class BaseCommandHandlerTest<THandler>
    {
        protected readonly Mock<ILogger<THandler>> _loggerMock;
        protected readonly Mock<INotificationContextService> _notificationContextServiceMock;
        protected readonly IMapper _mapper;
        protected readonly Fixture _fixture;

        protected BaseCommandHandlerTest()
        {
            _loggerMock = new();
            _notificationContextServiceMock = new();
            _mapper = CreateMapper();
            _fixture = new Fixture();
        }

        private IMapper CreateMapper()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddMaps(typeof(SignUpCommandHandlerProfile).Assembly);

            return new MapperConfiguration(mapperConfigurationExpression).CreateMapper();
        }

        protected abstract THandler MakeSut();
        protected abstract void SetupDefaultMocks();
    }
}
