using System;

namespace ApiMovimientos.Model
{
    public class Movimientos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Movimiento { get; set; }
        public Cuenta Cuenta { get; set; }
    }

    public enum TipoMovimiento
    {
        Deposito,
        Extracciones
    }
}
