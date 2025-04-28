using Microsoft.AspNetCore.Mvc;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MRent.WebApi.Controllers
{
    [Tags("entregadores")]
    [ApiController]
    [Route("entregadores")]
    public class CourierController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly ICourierService _courierService;

        public CourierController(ILogger<MotorcycleController> logger, ICourierService courierService)
        {
            _logger = logger;
            _courierService = courierService;
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Cadastrar entregador",
            Description = "Cadastra um entregador com os dados fornecidos",
            OperationId = "entregadores.cadastrar")]
        [SwaggerResponse(StatusCodes.Status201Created, "Criado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> Create([FromBody] CourierDTO entity)
        {
            _logger.Log(LogLevel.Information, "Cadastrando entregador com o cnpj {cnpj}", entity.cnpj);

            await _courierService.CreateAsync(entity);

            return Created();
        }

        [HttpPost("{id}/cnh")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Enviar foto da CNH",
            Description = "Atualiza a foto da CNH no cadastro do entregador",
            OperationId = "entregadores.fotocnh")]
        [SwaggerResponse(StatusCodes.Status201Created, "Criado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> UpdateCnh(string id, [FromForm] AtualizarImagem entity)
        {
            _logger.Log(LogLevel.Information, "Atualizando foto da cnh com id {id}", id);

            await _courierService.UpdateCnhImageAsync(Guid.Parse(id), entity.Imagem_cnh);

            return Created();
        }
    }
}
