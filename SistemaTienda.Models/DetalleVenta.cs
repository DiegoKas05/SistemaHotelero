using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.Models
{
    [Table("DETALLE_VENTA")]
    public class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }

        [ForeignKey("Venta")]
        public int IdVenta { get; set; }

        public Venta Venta { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal { get; set; }
    }
}

