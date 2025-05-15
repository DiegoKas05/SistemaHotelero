using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SistemaHotelero.Models

{
    [Table("ESTADO_HABITACION")]
    public class EstadoHabitacion
    {
        [Key]
        public int IdEstadoHabitacion { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(50, ErrorMessage = "La descripción no debe exceder los 50 caracteres.")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true; // Valor por defecto
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
