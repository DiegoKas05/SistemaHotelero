using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models.ViewModels;
using SistemaHotelero.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]

    //nuevaaa
    public class RecepcionController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public RecepcionController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll(
             includeProperties: "Categoria,Piso,EstadoHabitacion")
             .Where(h => h.Estado == true)
             .ToList();

            return View(habitaciones);
        }
        [HttpGet]
        public IActionResult Registrar(int id)
        {
            var habitacion = _contenedorTrabajo.Habitacion.GetFirstOrderDefault(
                h => h.IdHabitacion == id,
                includeProperties: "Categoria,Piso,EstadoHabitacion");

            if (habitacion == null)
                return NotFound();

            var viewModel = new ReservaVM
            {
                Habitacion = habitacion,
                ListaClientes = _contenedorTrabajo.ApplicationUser.ObtenerClientes()
                    .Select(u => new SelectListItem
                    {
                        Text = $"{u.Nombre} {u.Apellido} - {u.NumeroDocumento}",
                        Value = u.Id
                    }),
                Recepcion = new Recepcion
                {
                    IdHabitacion = id,
                    FechaEntrada = DateTime.Now,
                    PrecioInicial = habitacion.Precio
                }
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(ReservaVM viewModel)
        {
            if (viewModel.Recepcion.IdApplicationUser == null)
            {
                TempData["Error"] = "Debe seleccionar un cliente.";
                return RedirectToAction("Index");
            }

            viewModel.Recepcion.Estado = true;

            _contenedorTrabajo.Recepcion.Add(viewModel.Recepcion);

            var habitacionDb = _contenedorTrabajo.Habitacion.Get(viewModel.Recepcion.IdHabitacion);
            habitacionDb.IdEstadoHabitacion = 8;
            _contenedorTrabajo.Habitacion.Update(habitacionDb);

            _contenedorTrabajo.Save();

            TempData["Mensaje"] = "Reserva registrada correctamente.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var reserva = _contenedorTrabajo.Recepcion.GetAll(
            r => r.IdHabitacion == id && r.Estado == true,
            includeProperties: "Habitacion.Categoria,Habitacion.Piso,Habitacion.EstadoHabitacion,ApplicationUser"
)
            .OrderByDescending(r => r.FechaEntrada)
            .FirstOrDefault();


            if (reserva == null)
            {
                TempData["Error"] = "No se encontró una reserva activa para esta habitación.";
                return RedirectToAction("Index");
            }

            return View(reserva); // crearemos esta vista
        }

    }

}
