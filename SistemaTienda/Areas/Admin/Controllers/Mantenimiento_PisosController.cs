using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class Mantenimiento_PisosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public Mantenimiento_PisosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _contenedorTrabajo.Piso.GetAll();
            return Json(new { data = lista });
        }

        public IActionResult Crear()
        {
            return View(new Piso());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Piso piso)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Piso.Add(piso);
                _contenedorTrabajo.Save();
                TempData["success"] = "Piso creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(piso);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var piso = _contenedorTrabajo.Piso.Get(id);
            if (piso == null)
                return NotFound();

            return View(piso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Piso piso)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Piso.Update(piso);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Piso actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(piso);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var piso = _contenedorTrabajo.Piso.Get(id);
            if (piso == null)
            {
                return Json(new { success = false, message = "Error al eliminar el piso." });
            }

            _contenedorTrabajo.Piso.Remove(piso);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Piso eliminado correctamente." });
        }
    }
}