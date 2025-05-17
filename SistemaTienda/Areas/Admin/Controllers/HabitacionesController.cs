using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System;
using System.Linq;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class HabitacionesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HabitacionesController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create()
        {
            CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                // Inspeccionar errores de validación
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        var errors = string.Join(", ", state.Errors.Select(e => e.ErrorMessage));
                        Console.WriteLine($"Error de validación en {key}: {errors}");
                    }
                }
                CargarListasDesplegables();
                return View(habitacion);
            }

            // Validar que no exista habitación con el mismo número
            bool existeNumero = _contenedorTrabajo.Habitacion
                .GetAll()
                .Any(h => h.Numero == habitacion.Numero);

            if (existeNumero)
            {
                ModelState.AddModelError("Numero", "Ya existe una habitación con este número.");
                CargarListasDesplegables();
                return View(habitacion);
            }

            try
            {
                habitacion.FechaCreacion = DateTime.Now;
                habitacion.Estado = true;

                _contenedorTrabajo.Habitacion.Add(habitacion);
                _contenedorTrabajo.Save();

                TempData["Success"] = "Habitación creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Registrar el error completo para depuración
                Console.WriteLine($"Error al crear la habitación: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                ModelState.AddModelError("", "Error al crear la habitación: " + ex.Message);
                CargarListasDesplegables();
                return View(habitacion);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var habitacion = _contenedorTrabajo.Habitacion.Get(id);
            if (habitacion == null)
                return NotFound();

            CargarListasDesplegables();
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                var habitacionDesdeDb = _contenedorTrabajo.Habitacion.Get(habitacion.IdHabitacion);
                if (habitacionDesdeDb == null)
                    return NotFound();

                _contenedorTrabajo.Habitacion.Update(habitacion);
                _contenedorTrabajo.Save();

                TempData["Success"] = "Habitación editada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            CargarListasDesplegables();
            return View(habitacion);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll(
                includeProperties: "EstadoHabitacion,Piso,Categoria"
            );

            return Json(new { data = habitaciones });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var habitacion = _contenedorTrabajo.Habitacion.Get(id);
            if (habitacion == null)
                return Json(new { success = false, message = "Habitación no encontrada." });

            _contenedorTrabajo.Habitacion.Remove(habitacion);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Habitación eliminada correctamente." });
        }

        #endregion

        private void CargarListasDesplegables()
        {
            ViewBag.IdEstadoHabitacion = _contenedorTrabajo.EstadoHabitacion.GetAll()
                .Where(e => e.Estado)
                .Select(e => new SelectListItem
                {
                    Text = e.Descripcion,
                    Value = e.IdEstadoHabitacion.ToString()
                }).ToList();

            ViewBag.IdPiso = _contenedorTrabajo.Piso.GetAll()
                .Where(p => p.Estado)
                .Select(p => new SelectListItem
                {
                    Text = p.Descripcion,
                    Value = p.IdPiso.ToString()
                }).ToList();

            ViewBag.IdCategoria = _contenedorTrabajo.Categoria.GetAll()
                .Where(c => c.Estado)
                .Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.IdCategoria.ToString()
                }).ToList();
        }
    }
}
