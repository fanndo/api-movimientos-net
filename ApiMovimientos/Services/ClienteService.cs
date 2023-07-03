using ApiMovimientos.Contracts;
using ApiMovimientos.Dto;
using ApiMovimientos.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ICuentaRepository _cuentaRepository;
        public ClienteService(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }
        public async Task<CuentaDto> GetCuentaCliente(int id, int cuentaId)
        {
            CuentaDto result = null;
            try
            {
                var cuenta = await _cuentaRepository.GetCuentaCliente(id, cuentaId);
                //if (cuenta == null) throw new System.Exception("sin datos");
                result = new CuentaDto { NumeroCuenta = cuenta.NumeroCuenta, Saldo = cuenta.Saldo };
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IList<CuentaDto>> GetCuentasByClienteId(int id)
        {
            IList<CuentaDto> result = new List<CuentaDto>();
            try
            {
                var cuentas = await _cuentaRepository.GetAllByCliente(id);
                if (cuentas.Count == 0) throw new System.Exception("sin datos");
                foreach (var cuenta in cuentas)
                {
                    result.Add(new CuentaDto { NumeroCuenta = cuenta.NumeroCuenta, Saldo= cuenta.Saldo });
                } 
            }
            catch (System.Exception)
            {
                throw;
            }

            return result;
        }
    }
}
