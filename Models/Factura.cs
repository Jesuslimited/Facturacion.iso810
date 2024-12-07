using System;
using System.Collections.Generic;

namespace Facturacion.iso810.Models
{
    public partial class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string DescripcionArticulo { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string? Comentario { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int ArticuloId { get; set; }

        public virtual Articulo Articulo { get; set; } = null!;
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Vendedor Vendedor { get; set; } = null!;
    }
}
