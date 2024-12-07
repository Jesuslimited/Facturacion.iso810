using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PorcentajeComision { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
