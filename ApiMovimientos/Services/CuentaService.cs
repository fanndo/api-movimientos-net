using ApiMovimientos.Contracts;
using ApiMovimientos.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly Repository.ICuentaRepository _cuentaRepository;

        public CuentaService(Repository.ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        public async Task<IList<Cuenta>> GetAll()
        {
            return await _cuentaRepository.GetAll();
        }
    }
}
