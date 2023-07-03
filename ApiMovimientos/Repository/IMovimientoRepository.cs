using ApiMovimientos.Model;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public interface IMovimientoRepository
    {
        Task<Movimientos> GetByCuentaId(int cuentaId);
    }
}
