using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaHotelero.Models
{
    [Table("EMPLEADO")]
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [StringLength(15, ErrorMessage = "El tipo de documento no debe exceder los 15 caracteres.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(15, ErrorMessage = "El número de documento no debe exceder los 15 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de documento debe ser numérico.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre debe contener solo letras.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no debe exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido debe contener solo letras.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El correo no debe exceder los 50 caracteres.")]
        [EmailAddress(ErrorMessage = "Por favor ingresa un correo electrónico válido que contenga '@'.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La clave debe tener al menos 6 caracteres.")]
        public string Clave { get; set; }
    }
}
