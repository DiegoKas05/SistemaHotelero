using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Data;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class Productos2Controller : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;
        public Productos2Controller(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public FileResult Exportar(string estado, string fechaInicio, string fechaFin)
        {
            DateTime fi = DateTime.Parse(fechaInicio);
            DateTime ff = DateTime.Parse(fechaFin);

            // Obtener los productos filtrados por estado y fecha
            var productos = _contenedorTrabajo.Productos.GetAll(p =>
                (string.IsNullOrEmpty(estado) || p.Estado == (estado == "1")) &&
                p.FechaCreacion >= fi && p.FechaCreacion <= ff
            );

            // Crear tabla
            DataTable dt = new DataTable("Productos");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Detalle");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Fecha de Registro");

            foreach (var p in productos)
            {
                dt.Rows.Add(
                    p.Nombre,
                    p.Detalle,
                    p.Cantidad.ToString(),
                    p.Precio.ToString("F2"),
                    p.Estado ? "Activo" : "Inactivo",
                    p.FechaCreacion.ToString("yyyy-MM-dd")
                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Productos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    );
                }
            }
        }


    }
}