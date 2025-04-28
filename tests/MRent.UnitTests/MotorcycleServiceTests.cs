using Microsoft.Extensions.DependencyInjection;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.UnitTests.Fixtures;

namespace MRent.UnitTests
{
    public class MotorcycleServiceTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleServiceTests(IntegrationTestFixture fixture)
        {
            _motorcycleService = fixture.ServiceProvider.GetRequiredService<IMotorcycleService>();
        }

        [Fact]
        public async Task CreateMotorcycleAsync()
        {
            var motorcycle = new MotorcycleDTO
            {
                identificador = "ABC123",
                modelo = "Honda",
                ano = 2020,
                placa = "mqx123"
            };

            await _motorcycleService.CreateAsync(motorcycle);

            Assert.Equal(motorcycle.identificador, motorcycle.identificador);
        }
    }
}
