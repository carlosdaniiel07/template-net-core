using AutoFixture;
using AutoMapper;
using TemplateNetCore.Infra.Mapping;

namespace TemplateNetCore.Tests.Services
{
    public abstract class BaseTest
    {
        protected readonly Fixture _fixture;
        protected readonly IMapper _mapper;

        public BaseTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _mapper = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
    }
}
