using System.Collections.Generic;

namespace ApiMovimientos.Model
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cbu { get; set; }

        public string Alias { get; set; }
        public decimal Saldo { get; set; }
        public EstadoCuenta Status { get; set; }
        public Cliente Cliente { get; set; }    
        public IList<Movimientos> Movimientos { get; set; }
    }

    public enum EstadoCuenta
    {
        Habilitado,
        Deshabilitado
    }
}
