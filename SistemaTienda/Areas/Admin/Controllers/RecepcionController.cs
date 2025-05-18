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
                ListaClientes = _contenedorTrabajo.ApplicationUser.GetAll()
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
            if (!ModelState.IsValid)
            {
                var habitacion = _contenedorTrabajo.Habitacion.GetFirstOrderDefault(
                    h => h.IdHabitacion == viewModel.Recepcion.IdHabitacion,
                    includeProperties: "Categoria,Piso,EstadoHabitacion");

                viewModel.Habitacion = habitacion;
                viewModel.ListaClientes = _contenedorTrabajo.ApplicationUser.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Text = $"{u.Nombre} {u.Apellido} - {u.NumeroDocumento}",
                        Value = u.Id
                    });

                return View(viewModel);
            }

            viewModel.Recepcion.Estado = true;

            _contenedorTrabajo.Recepcion.Add(viewModel.Recepcion);

            var habitacionDb = _contenedorTrabajo.Habitacion.Get(viewModel.Recepcion.IdHabitacion);
            habitacionDb.IdEstadoHabitacion = 5; // ID del estado OCUPADO
            _contenedorTrabajo.Habitacion.Update(habitacionDb);

            _contenedorTrabajo.Save();

            TempData["Success"] = "Reserva registrada correctamente.";
            return RedirectToAction("Index");
        }



    }

}
