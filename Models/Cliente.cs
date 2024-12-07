using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            AsientoContables = new HashSet<AsientoContable>();
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CedulaRnc { get; set; } = null!;
        public string? CuentaContable { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<AsientoContable> AsientoContables { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
