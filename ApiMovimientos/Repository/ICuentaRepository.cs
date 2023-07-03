using ApiMovimientos.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public interface ICuentaRepository
    {
        Task<Cuenta> GetCuentaCliente(int id, int cuentaId);
        Task<IList<Cuenta>> GetAllByCliente(int id);
        Task<IList<Cuenta>> GetAll();
    }
}
