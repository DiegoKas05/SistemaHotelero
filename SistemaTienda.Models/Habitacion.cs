using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHotelero.Models
{
    [Table("HABITACION")]
    public class Habitacion
    {
        [Key]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de habitación no debe exceder los 50 caracteres.")]
        public string Numero { get; set; }

        [StringLength(100, ErrorMessage = "El detalle no debe exceder los 100 caracteres.")]
        public string Detalle { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Precio { get; set; }

        [ForeignKey("EstadoHabitacion")]
        public int IdEstadoHabitacion { get; set; }

        public EstadoHabitacion EstadoHabitacion { get; set; }

        [ForeignKey("Piso")]
        public int IdPiso { get; set; }

        public Piso Piso { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public Categoria Categoria { get; set; }

        public bool Estado { get; set; } = true; // Valor por defecto
    }
}
