using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Linq;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class PisosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public PisosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Piso piso)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Piso.Add(piso);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Piso creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(piso);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var piso = _contenedorTrabajo.Piso.Get(id);
            if (piso == null) return NotFound();
            return View(piso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Piso piso)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Piso.Update(piso);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Piso editado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(piso);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var pisos = _contenedorTrabajo.Piso.GetAll()
                            .OrderByDescending(p => p.IdPiso);
            return Json(new { data = pisos });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var piso = _contenedorTrabajo.Piso.Get(id);
            if (piso == null)
                return Json(new { success = false, message = "Piso no encontrado." });

            // Verificar si hay habitaciones asociadas a este piso
            bool tieneHabitaciones = _contenedorTrabajo.Habitacion.GetAll()
                .Any(h => h.IdPiso == id);

            if (tieneHabitaciones)
            {
                return Json(new
                {
                    success = false,
                    message = "No se puede eliminar el piso porque tiene habitaciones asociadas."
                });
            }

            _contenedorTrabajo.Piso.Remove(piso);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Piso eliminado correctamente." });
        }

        #endregion
    }
}
