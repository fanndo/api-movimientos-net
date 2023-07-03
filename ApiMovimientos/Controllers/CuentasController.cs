using ApiMovimientos.Contracts;
using ApiMovimientos.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMovimientos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : Controller
    {
        private readonly ICuentaRepository _cuentaRepository;
        public CuentasController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaldo(int id)
        {
            var result = await _cuentaRepository.GetAllByCliente(id);
            return Ok(result);
        }
    }
}
