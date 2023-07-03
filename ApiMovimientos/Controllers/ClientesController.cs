using ApiMovimientos.Contracts;
using ApiMovimientos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;


        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;

        }
        [HttpGet("{id}/cuentas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CuentaDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _clienteService.GetCuentasByClienteId(id);
            return Ok(result);
        }

        [HttpGet("{id}/cuenta/{cuentaId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CuentaDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetCuenta(int id, int cuentaId)
        {
            var result = await _clienteService.GetCuentaCliente(id,cuentaId);
            return Ok(result);
        }
    }
}
