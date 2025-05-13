using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.Models.ViewModels;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Empleado")]
    [Area("Admin")]
    public class VenderController : Controller
    {
        public IActionResult Index()
        {
            // Simulación de habitaciones
            var habitaciones = new List<dynamic>
            {
                new { Numero = "017", Categoria = "Individual", Piso = "1" },
                new { Numero = "016", Categoria = "Doble", Piso = "1" },
                new { Numero = "015", Categoria = "Suite", Piso = "2" },
                new { Numero = "014", Categoria = "Individual", Piso = "2" },
                new { Numero = "013", Categoria = "Suite", Piso = "2" }
            };

            return View(habitaciones);
        }
    }
}
