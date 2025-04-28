using Microsoft.Extensions.DependencyInjection;
using MRent.Application.Interfaces;
using MRent.Application.Services;

namespace MRent.UnitTests.Fixtures
{
    public class IntegrationTestFixture
    {
        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IMotorcycleService, MotorcycleService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
