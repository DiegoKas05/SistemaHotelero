using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.Models
{
    [Table("VENTA")]
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        [ForeignKey("Recepcion")]
        public int IdRecepcion { get; set; }

        public Recepcion Recepcion { get; set; }

        [Required]
        public decimal Total { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
