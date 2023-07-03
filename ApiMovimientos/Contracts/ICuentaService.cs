using ApiMovimientos.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Contracts
{
    public interface ICuentaService
    {
        Task<IList<Cuenta>> GetAll();
    }
}
