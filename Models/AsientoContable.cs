using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class AsientoContable
    {
        public int IdAsiento { get; set; }
        public string Descripcion { get; set; } = null!;
        public int ClienteId { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Cuentum Cuenta { get; set; } = null!;
    }
}
