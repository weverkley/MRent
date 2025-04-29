using Microsoft.AspNetCore.Mvc;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MRent.WebApi.Controllers
{
    [Tags("motos")]
    [ApiController]
    [Route("motos")]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(ILogger<MotorcycleController> logger, IMotorcycleService motorcycleService)
        {
            _logger = logger;
            _motorcycleService = motorcycleService;
        }

        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Consultar motos existentes",
            Description = "Retorna uma listagem de motos contendo todos os dados cadastrados",
            OperationId = "motos.listar")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de motos", typeof(IEnumerable<MotorcycleDTO>))]

        public async Task<IActionResult> GetAll([FromQuery] string? placa)
        {
            if (placa is not null)
            {
                _logger.Log(LogLevel.Information, "Consultando motos com placa {placa}", placa);
                var list = new List<MotorcycleDTO>();
                list.Add(await _motorcycleService.GetByPlateAsync(placa));
                return Ok(list);
            }
            else
            {
                _logger.Log(LogLevel.Information, "Consultando todas as motos");
                return Ok(await _motorcycleService.GetAllAsync());
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Consultar motos existentes por id",
            Description = "Retorna uma moto contendo todos os dados cadastrados",
            OperationId = "motos.buscar")]
        [SwaggerResponse(StatusCodes.Status200OK,
         "Detalhes da moto",
         typeof(MotorcycleDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Request mal formada", typeof(Retorno))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(Retorno))]
        public async Task<IActionResult> GetByIdentifier(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.Log(LogLevel.Error, "Id inválido");
                return BadRequest(new Retorno("Request mal formada"));
            }

            _logger.Log(LogLevel.Information, "Consultando moto com id {id}", id);

            if (Guid.TryParse(id, out var guid) == false)
            {
                _logger.Log(LogLevel.Error, "Id inválido");

                return BadRequest(new Retorno("Dados inválidos"));
            }

            var entity = await _motorcycleService.GetByIdAsync(guid);

            return Ok(entity);
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Cadastrar uma nova moto",
            Description = "Cadastra uma moto com os dados fornecidos",
            OperationId = "motos.cadastrar")]
        [SwaggerResponse(StatusCodes.Status201Created, "Criado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> Create([FromBody] MotorcycleDTO entity)
        {
            _logger.Log(LogLevel.Information, "Cadastrando moto com a placa {placa}", entity.placa);

            await _motorcycleService.CreateAsync(entity);

            return Created();
        }

        [HttpPut("{id}/placa")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Modificar a placa de uma moto",
            Description = "Modificar a placa de uma moto com a placa fornecida",
            OperationId = "motos.atualizar.placa")]
        [SwaggerResponse(StatusCodes.Status200OK, "Placa modificada com sucesso", typeof(Retorno))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> UpdatePlate(string id, [FromBody] AtualizarPlaca entity)
        {
            _logger.Log(LogLevel.Information, "Modificando moto com o identificador {id}", id);

            if (Guid.TryParse(id, out var guid) == false)
            {
                _logger.Log(LogLevel.Error, "Id inválido");

                return BadRequest(new Retorno("Dados inválidos"));
            }

            await _motorcycleService.UpdatePlateAsync(guid, entity.Placa);

            return Ok(new Retorno("Placa modificada com sucesso"));
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Remover uma moto",
            Description = "Remover uma moto com o id fornecida",
            OperationId = "motos.remover")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.Log(LogLevel.Information, "Deletar moto com o identificador {id}", id);

            if (Guid.TryParse(id, out var guid) == false)
            {
                _logger.Log(LogLevel.Error, "Id inválido");

                return BadRequest(new Retorno("Dados inválidos"));
            }

            await _motorcycleService.DeleteAsync(guid);

            return Ok();
        }
    }
}
