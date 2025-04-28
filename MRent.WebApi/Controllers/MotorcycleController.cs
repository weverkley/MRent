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

        public async Task<IActionResult> GetAll([FromQuery] string placa)
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

            var entity = await _motorcycleService.GetByIdentifierAsync(id);

            if (entity is null)
            {
                _logger.Log(LogLevel.Error, "Moto com id {id} não encontrada", id);
                return NotFound(new Retorno("Moto não encontrada"));
            }

            return Ok(entity);
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Cadastrar moto",
            Description = "Cadastra uma moto com os dados fornecidos",
            OperationId = "motos.cadastrar")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Retorno))]
        public async Task<IActionResult> Create([FromBody] MotorcycleDTO entity)
        {
            _logger.Log(LogLevel.Information, "Cadastrando moto com a placa {placa}", entity.placa);

            await _motorcycleService.CreateAsync(entity);

            return Created();
        }
    }
}
