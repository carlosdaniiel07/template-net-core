using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TemplateNetCore.Application.Commands.v1.Auth.SignUp;

namespace TemplateNetCore.Tests.Shared;

public abstract class BaseTest<TClass> where TClass : class
{
    protected readonly Mock<ILogger<TClass>> _loggerMock;
    protected readonly IMapper _mapper;
    protected readonly Fixture _fixture;

    protected BaseTest()
    {
        _loggerMock = new();
        _mapper = CreateMapper();
        _fixture = new Fixture();
    }

    private IMapper CreateMapper()
    {
        var mapperConfigurationExpression = new MapperConfigurationExpression();
        mapperConfigurationExpression.AddMaps(typeof(SignUpCommandProfile).Assembly);

        return new MapperConfiguration(mapperConfigurationExpression).CreateMapper();
    }

    protected abstract TClass MakeSut();
    protected abstract void SetupDefaultMocks();
}
