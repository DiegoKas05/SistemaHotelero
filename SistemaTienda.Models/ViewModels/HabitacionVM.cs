using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHotelero.Models;

namespace SistemaHotelero.Models.ViewModels
{
    public class HabitacionVM
    {
        public Habitacion Habitacion { get; set; }
        public IEnumerable<SelectListItem> ListaCategoria { get; set; }
        public IEnumerable<SelectListItem> ListaPiso { get; set; }
        public IEnumerable<SelectListItem> ListaEstado { get; set; }
    }
}
