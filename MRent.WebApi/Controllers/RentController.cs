using Microsoft.AspNetCore.Mvc;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MRent.WebApi.Controllers
{
    [Tags("locacao")]
    [ApiController]
    [Route("locacao")]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;
        private readonly IRentService _rentService;

        public RentController(ILogger<RentController> logger, IRentService rentService)
        {
            _logger = logger;
            _rentService = rentService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Consultar loca��o por id",
            Description = "Retorna uma loca��o contendo todos os dados cadastrados",
            OperationId = "locacao.buscar")]
        [SwaggerResponse(StatusCodes.Status200OK,
         "Detalhes da loca��o",
         typeof(RentDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inv�lidos", typeof(Retorno))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Dados n�o encontrados", typeof(Retorno))]
        public async Task<IActionResult> GetByIdentifier(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.Log(LogLevel.Error, "Id inv�lido");
                return BadRequest(new Retorno("Request mal formada"));
            }

            _logger.Log(LogLevel.Information, "Consultando loca��o com id {id}", id);

            if (Guid.TryParse(id, out var guid) == false)
            {
                _logger.Log(LogLevel.Error, "Id inv�lido");

                return BadRequest(new Retorno("Dados inv�lidos"));
            }

            var entity = await _rentService.GetByIdAsync(guid);

            return Ok(entity);
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Alugar uma moto",
            Description = "Cadastra um aluguel de uma moto com dados fornecidos",
            OperationId = "locacao.cadastrar")]
        [SwaggerResponse(StatusCodes.Status201Created, "Criado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inv�lidos", typeof(Retorno))]
        public async Task<IActionResult> Create([FromBody] RentDTO entity)
        {
            _logger.Log(LogLevel.Information, "Cadastrando loca��o com a moto_id {moto_id}", entity.moto_id);

            await _rentService.CreateAsync(entity);

            return Created();
        }

        [HttpPut("{id}/devolucao")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Informar data de devolu��o e calcular valor",
            Description = "Calcular taxas de devolu��o de uma moto",
            OperationId = "motos.atualizar.placa")]
        [SwaggerResponse(StatusCodes.Status200OK, "Data de devolu��o informada com sucesso", typeof(Retorno))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inv�lidos", typeof(Retorno))]
        public async Task<IActionResult> UpdateReturnDate(string id, [FromBody] AtualizarDataDevolucao entity)
        {
            _logger.Log(LogLevel.Information, "Atualizar data de devolu��o para o identificador {id}", id);

            if (Guid.TryParse(id, out var guid) == false)
            {
                _logger.Log(LogLevel.Error, "Id inv�lido");

                return BadRequest(new Retorno("Dados inv�lidos"));
            }

            await _rentService.UpdateReturnDateAsync(guid, entity.data_devolucao);

            return Ok(new Retorno("Data de devolu��o informada com sucesso"));
        }
    }
}
