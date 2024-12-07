using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
