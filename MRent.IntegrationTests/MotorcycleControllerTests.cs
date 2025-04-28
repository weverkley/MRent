using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MRent.Application.DTO;
using MRent.Infrastructure.Contexts;

namespace MRent.IntegrationTests
{
    public class MotorcycleControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MotorcycleControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the real DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PostgresContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Add an in-memory DbContext for testing
                    services.AddDbContext<PostgresContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });

                    // Seed data if necessary
                    using (var scope = services.BuildServiceProvider().CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<PostgresContext>();
                        db.Database.EnsureCreated();
                    }
                });
            });
        }

        [Fact]
        public async Task GetMotos_ReturnsOk()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/motos");

            response.EnsureSuccessStatusCode();
            var motos = await response.Content.ReadFromJsonAsync<List<MotorcycleDTO>>();
            Assert.NotNull(motos);
        }

        [Fact]
        public async Task CreateMoto_ReturnsCreatedMoto()
        {
            var client = _factory.CreateClient();

            var newMoto = new MotorcycleDTO
            {
                identificador = Guid.NewGuid().ToString(),
                ano = 2024,
                modelo = "Yamaha R1",
                placa = "ABC1D23"
            };

            var response = await client.PostAsJsonAsync("/motos", newMoto);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var createdMoto = await response.Content.ReadFromJsonAsync<MotorcycleDTO>();
            Assert.NotNull(createdMoto);
            Assert.Equal(newMoto.modelo, createdMoto!.modelo);
        }

        [Fact]
        public async Task GetMoto_ReturnsMoto()
        {
            var client = _factory.CreateClient();

            var newMoto = new MotorcycleDTO
            {
                identificador = Guid.NewGuid().ToString(),
                ano = 2023,
                modelo = "Ducati Panigale",
                placa = "XYZ4W56"
            };

            var createResponse = await client.PostAsJsonAsync("/motos", newMoto);
            createResponse.EnsureSuccessStatusCode();
            var createdMoto = await createResponse.Content.ReadFromJsonAsync<MotorcycleDTO>();

            var getResponse = await client.GetAsync($"/motos/{createdMoto!.identificador}");
            getResponse.EnsureSuccessStatusCode();
            var fetchedMoto = await getResponse.Content.ReadFromJsonAsync<MotorcycleDTO>();

            Assert.Equal(newMoto.modelo, fetchedMoto!.modelo);
        }

        [Fact]
        public async Task UpdateMoto_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var newMoto = new MotorcycleDTO
            {
                identificador = Guid.NewGuid().ToString(),
                ano = 2022,
                modelo = "Kawasaki Ninja",
                placa = "JKL9M01"
            };

            var createResponse = await client.PostAsJsonAsync("/motos", newMoto);
            createResponse.EnsureSuccessStatusCode();
            var createdMoto = await createResponse.Content.ReadFromJsonAsync<MotorcycleDTO>();

            // Update
            createdMoto!.modelo = "Kawasaki Ninja ZX-10R";
            var updateResponse = await client.PutAsJsonAsync($"/motos/{createdMoto.identificador}", createdMoto);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, updateResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteMoto_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var newMoto = new MotorcycleDTO
            {
                identificador = Guid.NewGuid().ToString(),
                ano = 2021,
                modelo = "Honda CBR",
                placa = "DEF2G34"
            };

            var createResponse = await client.PostAsJsonAsync("/motos", newMoto);
            createResponse.EnsureSuccessStatusCode();
            var createdMoto = await createResponse.Content.ReadFromJsonAsync<MotorcycleDTO>();

            var deleteResponse = await client.DeleteAsync($"/motos/{createdMoto!.identificador}");

            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
