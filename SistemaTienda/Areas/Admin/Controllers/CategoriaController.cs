using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Linq;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Categoría creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Categoría editada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var categorias = _contenedorTrabajo.Categoria.GetAll()
                               .OrderByDescending(c => c.IdCategoria);
            return Json(new { data = categorias });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
                return Json(new { success = false, message = "Categoría no encontrada." });

            // Verificar si hay habitaciones asociadas a esta categoría
            bool tieneHabitaciones = _contenedorTrabajo.Habitacion.GetAll()
                .Any(h => h.IdCategoria == id);

            if (tieneHabitaciones)
            {
                return Json(new
                {
                    success = false,
                    message = "No se puede eliminar la categoría porque tiene habitaciones asociadas."
                });
            }

            _contenedorTrabajo.Categoria.Remove(categoria);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoría eliminada correctamente." });
        }

        #endregion
    }
}
