using System.Collections.Generic;

namespace ApiMovimientos.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cuil { get; set; }
        public string Nombre { get; set; }
        public string Apellido  { get; set; }
        //public IList<Cuenta> Cuentas { get; set; }

    }
}
