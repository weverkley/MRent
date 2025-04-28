using Microsoft.OpenApi.Models;
using MRent.Application.Injectors;
using MRent.EventBus.Injectors;
using MRent.Infrastructure.Injectors;
using MRent.WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MRent API",
        Version = "v1",
        Description = "MRent é uma Web Api feita para demonstrar o processo de aluguel de motos.",
        Contact = new OpenApiContact
        {
            Name = "Wever Kley",
            Url = new Uri("https://github.com/weverkley/MRent"),
        },
        License = new OpenApiLicense
        {
            Name = "The API License",
            Url = new Uri("https://github.com/weverkley/MRent/blob/main/LICENSE"),
        }
    });
});
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.RegisterMassTransitAsEventBus(builder.Configuration);

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
