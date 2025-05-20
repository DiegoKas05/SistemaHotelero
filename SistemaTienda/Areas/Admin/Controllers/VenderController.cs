using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models.ViewModels;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class VenderController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public VenderController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo ?? throw new ArgumentNullException(nameof(contenedorTrabajo));
        }

        public IActionResult Index()
        {
            var habitacionesOcupadas = _contenedorTrabajo.Habitacion.GetAll(
                h => h.Estado == true && h.EstadoHabitacion.Descripcion == "Ocupado",
                includeProperties: "Categoria,Piso,EstadoHabitacion"
            ).ToList();

            return View(habitacionesOcupadas);
        }
    }
}
