using ApiMovimientos.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public class CuentaRepository : ICuentaRepository
    {
        private static IList<Cuenta> _cuentas = new List<Cuenta>();

        static CuentaRepository()
        {
            var cliente = new Cliente { Id = 1, Cuil = "20-23445554-0", Apellido = "Gonzales", Nombre = "Fernando" };

            _cuentas.Add(new Cuenta {  Cliente = cliente, Id = 1, Alias = "nano0", Cbu = "12444", Saldo = 1344, NumeroCuenta = "CA-091134-0998", Status = EstadoCuenta.Habilitado }) ;
            _cuentas.Add(new Cuenta {  Cliente = cliente, Id = 2, Alias = "nano1", Cbu = "12444", Saldo = 1344, NumeroCuenta = "CA-191134-0998", Status = EstadoCuenta.Habilitado });
            _cuentas.Add(new Cuenta {  Cliente = cliente, Id = 3, Alias = "nano2", Cbu = "12444", Saldo = 1344, NumeroCuenta = "CA-291134-0998", Status = EstadoCuenta.Habilitado });
            _cuentas.Add(new Cuenta {  Cliente = cliente, Id = 4, Alias = "nano3", Cbu = "12444", Saldo = 1344, NumeroCuenta = "CA-391134-0998", Status = EstadoCuenta.Habilitado });
        }

        public Task<Cuenta> GetCuentaCliente(int id, int cuentaId)
        {
            var cuenta = _cuentas.Where(c => c.Id == cuentaId && c.Cliente.Id == id).FirstOrDefault();
            return Task.FromResult(cuenta);
        }

        public Task<IList<Cuenta>> GetAllByCliente(int id)
        {
            IList<Cuenta> cuenta = _cuentas.Where(c => c.Cliente.Id == id).ToList();

            return Task.FromResult(cuenta);
        }

        public Task<IList<Cuenta>> GetAll()
        {
            return Task.FromResult(_cuentas);
        }


    }
}
