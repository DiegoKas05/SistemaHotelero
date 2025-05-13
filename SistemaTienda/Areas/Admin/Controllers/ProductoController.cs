using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ProductosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Productos.Add(producto);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producto = _contenedorTrabajo.Productos.Get(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Productos.Update(producto);
                _contenedorTrabajo.Save();
                TempData["Success"] = "Producto editado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var productos = _contenedorTrabajo.Productos.GetAll()
                             .OrderByDescending(p => p.IdProducto);
            return Json(new { data = productos });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var producto = _contenedorTrabajo.Productos.Get(id);
            if (producto == null)
                return Json(new { success = false, message = "Producto no encontrado." });

            _contenedorTrabajo.Productos.Remove(producto);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Producto eliminado correctamente." });
        }

        #endregion
    }
}
