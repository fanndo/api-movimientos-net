using ApiMovimientos.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Contracts
{
    public interface IClienteService
    {
        Task<IList<CuentaDto>> GetCuentasByClienteId(int id);
        Task<CuentaDto> GetCuentaCliente(int id, int cuentaId);

    }
}
