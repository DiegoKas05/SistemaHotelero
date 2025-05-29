using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaHotelero.Models
{
    [Table("HABITACION")]
    public class Habitacion
    {
        [Key]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ser un valor positivo.")]
        public string Numero { get; set; }

        [StringLength(100, ErrorMessage = "El detalle no debe exceder los 100 caracteres.")]
        public string Detalle { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Precio { get; set; }

        // FK explícitas
        [Required(ErrorMessage = "El estado de la habitación es obligatorio.")]
        public int IdEstadoHabitacion { get; set; }

        [ForeignKey("IdEstadoHabitacion")]
        public EstadoHabitacion? EstadoHabitacion { get; set; } // Hacer nullable

        [Required(ErrorMessage = "El piso es obligatorio.")]
        public int IdPiso { get; set; }

        [ForeignKey("IdPiso")]
        public Piso? Piso { get; set; } // Hacer nullable

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; } // Hacer nullable

        public bool Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}