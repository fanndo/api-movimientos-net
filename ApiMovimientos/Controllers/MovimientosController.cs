using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiMovimientos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : Controller
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = "";
            return Ok(result);
        }
    }
}
