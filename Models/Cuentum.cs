using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class Cuentum
    {
        public Cuentum()
        {
            AsientoContables = new HashSet<AsientoContable>();
        }

        public int Id { get; set; }
        public string NoCuenta { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<AsientoContable> AsientoContables { get; set; }
    }
}
