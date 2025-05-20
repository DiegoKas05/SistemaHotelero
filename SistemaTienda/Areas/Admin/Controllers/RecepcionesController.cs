using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Data;
using ClosedXML.Excel;
using SistemaHotelero.Models.ViewModels;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class RecepcionesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;
        public RecepcionesController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;

        }

        public IActionResult Index()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll()
              .Select(h => h.Numero)
              .Distinct()
              .ToList();

            var vm = new RecepcionFiltroVM
            {
                Habitaciones = habitaciones
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public FileResult Exportar(string habitacion, string fechaInicio, string fechaFin)
        {
            DateTime fi = DateTime.Parse(fechaInicio);
            DateTime ff = DateTime.Parse(fechaFin);

            var datos = _contenedorTrabajo.Recepcion.GetAll(r =>
                r.Estado == true && // Habitaciones ocupadas
                r.FechaEntrada >= fi && r.FechaEntrada <= ff &&
                (string.IsNullOrEmpty(habitacion) || r.Habitacion.Numero == habitacion),
                 includeProperties: "Habitacion.Categoria,ApplicationUser,Habitacion"
            );

            DataTable dt = new DataTable("Recepciones");

            dt.Columns.Add("Número");
            dt.Columns.Add("Detalle");
            dt.Columns.Add("Categoría");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Correo");
            dt.Columns.Add("Fecha Entrada");
            dt.Columns.Add("Fecha Salida");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Adelanto");
            dt.Columns.Add("Costo Penalidad");
            dt.Columns.Add("Total Pagado");


            foreach (var item in datos)
            {
                dt.Rows.Add(
                item.Habitacion.Numero,
                item.Habitacion.Detalle,
                item.Habitacion.Categoria.Descripcion,
                item.ApplicationUser.Nombre + " " + item.ApplicationUser.Apellido,
                item.ApplicationUser.Email,
                item.FechaEntrada.ToString("yyyy-MM-dd"),
                item.FechaSalida?.ToString("yyyy-MM-dd") ?? "Pendiente",
                item.PrecioInicial.ToString("F2"),
                item.Adelanto.ToString("F2"),
                item.CostoPenalidad.ToString("F2"),
                 item.TotalPagado.ToString("F2")

                );

            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Recepciones_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                }
            }
        }


    }
}