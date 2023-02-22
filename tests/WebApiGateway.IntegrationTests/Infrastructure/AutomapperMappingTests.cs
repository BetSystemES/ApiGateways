using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiGateway.IntegrationTests.Infrastructure
{
    public class AutomapperMappingTests : IClassFixture<WebAppFactory>, IDisposable
    {
        private readonly IServiceScope _scope;

        private readonly IMapper _mapper;

        public AutomapperMappingTests(WebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _mapper = _scope.ServiceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public void AutoMapper_Configuration_Should_BeValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
