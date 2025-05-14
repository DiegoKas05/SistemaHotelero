using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models.ViewModels;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public DashboardController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        //este es nuestro index
        public IActionResult Index()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll(
                includeProperties: "EstadoHabitacion"
            ).Where(h => h.Estado == true).ToList();

            var viewModel = new DashboardViewModel
            {
                TotalHabitaciones = habitaciones.Count,
                HabitacionesDisponibles = habitaciones.Count(h => h.EstadoHabitacion.Descripcion.ToLower() == "disponible"),
                HabitacionesOcupadas = habitaciones.Count(h => h.EstadoHabitacion.Descripcion.ToLower() == "ocupado"),
                HabitacionesLimpieza = habitaciones.Count(h => h.EstadoHabitacion.Descripcion.ToLower() == "limpieza")
            };

            return View(viewModel);
        }
    }
}

