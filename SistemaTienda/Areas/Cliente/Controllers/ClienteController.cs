using Microsoft.AspNetCore.Mvc;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Area("Cliente")]
    public class ashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
