using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using SistemaHotelero.Models.ViewModels;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class Mantenimiento_HabController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public Mantenimiento_HabController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            HabitacionVM habitacionVM = new HabitacionVM()
            {
                Habitacion = new Habitacion(),
                ListaCategoria = _contenedorTrabajo.Categoria
                    .GetAll().Where(c => c.Estado)
                    .Select(c => new SelectListItem
                    {
                        Text = c.Descripcion,
                        Value = c.IdCategoria.ToString()
                    }),

                ListaPiso = _contenedorTrabajo.Piso
                    .GetAll().Where(p => p.Estado)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Descripcion,
                        Value = p.IdPiso.ToString()
                    }),

                ListaEstado = _contenedorTrabajo.EstadoHabitacion
                    .GetAll().Where(e => e.Estado)
                    .Select(e => new SelectListItem
                    {
                        Text = e.Descripcion,
                        Value = e.IdEstadoHabitacion.ToString()
                    })
            };

            return View(habitacionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(HabitacionVM habitacionVM)
        {
            if (ModelState.IsValid)
            {
                // Log para ver los valores antes de guardar
                Console.WriteLine($"Número: {habitacionVM.Habitacion.Numero}, Precio: {habitacionVM.Habitacion.Precio}, Categoria: {habitacionVM.Habitacion.IdCategoria}");

                try
                {
                    // Agregar la nueva habitación
                    _contenedorTrabajo.Habitacion.Add(habitacionVM.Habitacion);
                    _contenedorTrabajo.Save(); // Guardar en la base de datos

                    TempData["Success"] = "Habitación creada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    TempData["Error"] = $"Hubo un error al crear la habitación: {ex.Message}";
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                // Mostrar errores de validación
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Puedes usar un log aquí también
                }

                TempData["Error"] = "No se pudo crear la habitación. Revisa los campos.";
            }

            // Recargar listas si ocurre un error
            habitacionVM.ListaCategoria = _contenedorTrabajo.Categoria
                .GetAll().Where(c => c.Estado)
                .Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.IdCategoria.ToString()
                });

            habitacionVM.ListaPiso = _contenedorTrabajo.Piso
                .GetAll().Where(p => p.Estado)
                .Select(p => new SelectListItem
                {
                    Text = p.Descripcion,
                    Value = p.IdPiso.ToString()
                });

            habitacionVM.ListaEstado = _contenedorTrabajo.EstadoHabitacion
                .GetAll().Where(e => e.Estado)
                .Select(e => new SelectListItem
                {
                    Text = e.Descripcion,
                    Value = e.IdEstadoHabitacion.ToString()
                });

            return View(habitacionVM);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var habitacion = _contenedorTrabajo.Habitacion.Get(id);
            if (habitacion == null)
                return NotFound();

            HabitacionVM habitacionVM = new HabitacionVM()
            {
                Habitacion = habitacion,
                ListaCategoria = _contenedorTrabajo.Categoria
                    .GetAll().Where(c => c.Estado)
                    .Select(c => new SelectListItem
                    {
                        Text = c.Descripcion,
                        Value = c.IdCategoria.ToString()
                    }),

                ListaPiso = _contenedorTrabajo.Piso
                    .GetAll().Where(p => p.Estado)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Descripcion,
                        Value = p.IdPiso.ToString()
                    }),

                ListaEstado = _contenedorTrabajo.EstadoHabitacion
                    .GetAll().Where(e => e.Estado)
                    .Select(e => new SelectListItem
                    {
                        Text = e.Descripcion,
                        Value = e.IdEstadoHabitacion.ToString()
                    })
            };

            return View(habitacionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(HabitacionVM habitacionVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contenedorTrabajo.Habitacion.Update(habitacionVM.Habitacion);
                    _contenedorTrabajo.Save();

                    TempData["Success"] = "Habitación actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Hubo un error al actualizar la habitación: {ex.Message}";
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            habitacionVM.ListaCategoria = _contenedorTrabajo.Categoria
                .GetAll().Where(c => c.Estado)
                .Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.IdCategoria.ToString()
                });

            habitacionVM.ListaPiso = _contenedorTrabajo.Piso
                .GetAll().Where(p => p.Estado)
                .Select(p => new SelectListItem
                {
                    Text = p.Descripcion,
                    Value = p.IdPiso.ToString()
                });

            habitacionVM.ListaEstado = _contenedorTrabajo.EstadoHabitacion
                .GetAll().Where(e => e.Estado)
                .Select(e => new SelectListItem
                {
                    Text = e.Descripcion,
                    Value = e.IdEstadoHabitacion.ToString()
                });

            return View(habitacionVM);
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            var habitacion = _contenedorTrabajo.Habitacion.Get(id);
            if (habitacion == null)
            {
                return Json(new { success = false, message = "Habitación no encontrada" });
            }

            _contenedorTrabajo.Habitacion.Remove(habitacion);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Habitación eliminada correctamente" });
        }

        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Habitacion.GetAll() });
        }
        #endregion
    }
}
