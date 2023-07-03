using ApiMovimientos.Model;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        public Task<Movimientos> GetByCuentaId(int cuentaId)
        {
            throw new System.NotImplementedException();
        }
    }
}
