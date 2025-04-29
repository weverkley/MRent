using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MRent.Application.Commands.Courier;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.Domain.Enums;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;
using MRent.EventBus.Interfaces;

namespace MRent.Application.Services
{
    public class CourierService : ICourierService
    {
        private readonly IEventBus _eventBus;
        private readonly IValidator<CreateCourierCommand> _createValidator;
        private readonly IValidator<UpdateCourierImageCNHCommand> _updateImageValidator;
        private readonly IMinioService _minioService;
        private readonly IConfiguration _configuration;

        public CourierService(IEventBus eventBus,
            IValidator<CreateCourierCommand> createValidator,
            IValidator<UpdateCourierImageCNHCommand> updateImageValidator,
            ICourierRepository courierRepository,
            IRentRepository rentRepository,
            IMinioService minioService,
            IConfiguration configuration)
        {
            _eventBus = eventBus;
            _createValidator = createValidator;
            _updateImageValidator = updateImageValidator;
            _minioService = minioService;
            _configuration = configuration;
        }

        public async Task CreateAsync(CourierDTO entity)
        {
            var command = new CreateCourierCommand
            {
                Name = entity.nome,
                CNPJ = entity.cnpj,
                BornDate = entity.data_nascimento,
                CNH = entity.numero_cnh,
                CNHType = ConvertCNHType(entity.tipo_cnh),
            };

            var validation = await _createValidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                throw new CourierValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        public async Task UpdateCnhImageAsync(Guid id, IFormFile image)
        {
            if (image is null)
            {
                throw new CourierValidationException("Dados inválidos");
            }

            await _minioService.EnsureBucketExistsAsync(_configuration["Minio:BucketName"]);
            await _minioService.SetBucketPublicAsync(_configuration["Minio:BucketName"]);

            string extension = Path.GetExtension(image.FileName).ToLower();

            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");

            var fileName = $"cnh-{uniqueId}{extension}";

            await _minioService.UploadFileAsync(image, fileName);

            var command = new UpdateCourierImageCNHCommand
            {
                Id = id,
                CNHImage = string.Format("{0}/{1}", _configuration["Minio:BucketName"], fileName),
            };

            var validation = await _updateImageValidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                throw new CourierValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        private static ECNHType ConvertCNHType(string cnhType)
        {
            return cnhType switch
            {
                "A" => ECNHType.A,
                "B" => ECNHType.B,
                "A+B" => ECNHType.AB,
                _ => throw new CourierValidationException("Tipo de CNH inválido")
            };
        }
    }
}
