using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.Models
{
    [Table("RECEPCION")]
    public class Recepcion
    {
        [Key]
        public int IdRecepcion { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        [ForeignKey("Habitacion")]
        public int IdHabitacion { get; set; }

        public Habitacion Habitacion { get; set; }

        public DateTime FechaEntrada { get; set; } = DateTime.Now;

        public DateTime? FechaSalida { get; set; }

        public DateTime? FechaSalidaConfirmacion { get; set; }

        public decimal PrecioInicial { get; set; }

        public decimal Adelanto { get; set; }

        public decimal PrecioRestante { get; set; }

        public decimal TotalPagado { get; set; } = 0;

        public decimal CostoPenalidad { get; set; } = 0;

        [StringLength(500)]
        public string Observacion { get; set; }

        public bool Estado { get; set; }
    }
}
