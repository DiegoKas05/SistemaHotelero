using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Linq;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class SalidaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public SalidaController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll(
                includeProperties: "Categoria,Piso,EstadoHabitacion")
                .Where(h => h.Estado == true && h.IdEstadoHabitacion == 2) // 2 = Ocupado
                .ToList();

            return View(habitaciones);
        }

        [HttpGet]
        public IActionResult RegistrarSalida(int id)
        {
            var reserva = _contenedorTrabajo.Recepcion.GetAll(
                r => r.IdHabitacion == id && r.Estado == true,
                includeProperties: "Habitacion.Categoria,Habitacion.Piso,Habitacion.EstadoHabitacion,ApplicationUser"
            ).FirstOrDefault();

            if (reserva == null)
            {
                TempData["Error"] = "No se encontró una reserva activa para esta habitación.";
                return RedirectToAction("Index");
            }

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarSalida(int idRecepcion, decimal montoPagado)
        {
            var recepcion = _contenedorTrabajo.Recepcion.Get(idRecepcion);

            if (recepcion == null)
            {
                TempData["Error"] = "No se encontró la reserva.";
                return RedirectToAction("Index");
            }

            // Actualizar la recepción
            recepcion.Estado = false; // Marcar como finalizada
            recepcion.FechaSalida = DateTime.Now;

            // Aquí marcamos como pagado el total
            recepcion.TotalPagado = recepcion.PrecioInicial;

            _contenedorTrabajo.Recepcion.Actualizar(recepcion);

            // Actualizar la habitación a limpieza
            var habitacion = _contenedorTrabajo.Habitacion.Get(recepcion.IdHabitacion);
            habitacion.IdEstadoHabitacion = 3; // 3 = Limpieza
            _contenedorTrabajo.Habitacion.Update(habitacion);

            _contenedorTrabajo.Save();

            TempData["Mensaje"] = "Salida registrada correctamente y habitación marcada como limpieza.";
            return RedirectToAction("Index");
        }

    }
}