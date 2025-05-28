using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
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

        [HttpGet]
        public IActionResult Registrar(int id)
        {
            var reserva = _contenedorTrabajo.Recepcion.GetAll(
                r => r.IdHabitacion == id && r.Estado == true,
                includeProperties: "Habitacion.Categoria,Habitacion.Piso,ApplicationUser"
            ).OrderByDescending(r => r.FechaEntrada).FirstOrDefault();

            if (reserva == null)
                return NotFound();

            var viewModel = new VentaVM
            {
                IdRecepcion = reserva.IdRecepcion,
                Habitacion = reserva.Habitacion,
                Cliente = reserva.ApplicationUser,
                ListaProductos = _contenedorTrabajo.Productos.GetAll()
                    .Where(p => p.Estado == true)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.IdProducto.ToString()
                    }),
                Venta = new Venta(),
                DetalleVenta = new List<DetalleVenta>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(int id, string DetalleVentaJson)
        {
            // id = IdRecepcion
            if (string.IsNullOrEmpty(DetalleVentaJson))
            {
                TempData["Error"] = "Debe agregar al menos un producto.";
                return RedirectToAction(nameof(Registrar), new { id = id });
            }

            var detalles = System.Text.Json.JsonSerializer.Deserialize<List<DetalleVentaTemp>>(DetalleVentaJson);

            if (detalles == null || detalles.Count == 0)
            {
                TempData["Error"] = "Debe agregar al menos un producto.";
                return RedirectToAction(nameof(Registrar), new { id = id });
            }

            // 1. VALIDACIÓN DE STOCK
            foreach (var item in detalles)
            {
                var producto = _contenedorTrabajo.Productos.Get(item.idProducto);
                if (producto == null)
                {
                    TempData["Error"] = $"El producto seleccionado no existe.";
                    return RedirectToAction(nameof(Registrar), new { id = id });
                }
                if (producto.Cantidad< item.cantidad)
                {
                    TempData["Error"] = $"Stock insuficiente para '{producto.Nombre}'. Disponibles: {producto.Cantidad}, solicitados: {item.cantidad}";
                    return RedirectToAction(nameof(Registrar), new { id = id });
                }
            }

            decimal total = detalles.Sum(x => x.subTotal);

            var venta = new Venta
            {
                IdRecepcion = id,
                Total = total,
                Estado = "Realizada"
                // FechaCreacion se asigna automáticamente
            };

            _contenedorTrabajo.Venta.Add(venta);
            _contenedorTrabajo.Save();

            foreach (var item in detalles)
            {
                var detalle = new DetalleVenta
                {
                    IdVenta = venta.IdVenta,
                    IdProducto = item.idProducto,
                    Cantidad = item.cantidad,
                    SubTotal = item.subTotal
                };
                _contenedorTrabajo.DetalleVenta.Add(detalle);

                // 2. ACTUALIZAR STOCK DEL PRODUCTO
                var producto = _contenedorTrabajo.Productos.Get(item.idProducto);
                if (producto != null)
                {
                    producto.Cantidad -= item.cantidad;
                    if (producto.Cantidad <= 0)
                    {
                        producto.Cantidad = 0;
                        producto.Estado = false; // Cambia a inactivo
                    }
                    _contenedorTrabajo.Productos.Update(producto);
                }
            }

                TempData["Exito"] = "Venta registrada correctamente y stock actualizado.";
            return RedirectToAction(nameof(Index));
        }


        public class DetalleVentaTemp
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public decimal subTotal { get; set; }
        }

        [HttpGet]
        public IActionResult GetPrecioProducto(int id)
        {
            var producto = _contenedorTrabajo.Productos.GetFirstOrDefault(p => p.IdProducto == id);
            return Json(producto != null ? producto.Precio : 0);
        }

        [HttpGet]
        public IActionResult GetStockProducto(int id)
        {
            var producto = _contenedorTrabajo.Productos.GetFirstOrDefault(p => p.IdProducto == id);
            return Json(producto != null ? producto.Cantidad : 0);
        }
    }
}