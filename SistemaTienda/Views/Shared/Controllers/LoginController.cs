using Microsoft.AspNetCore.Mvc;

namespace SistemaHotelero.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Identity/Account/Login");
        }
    }
}
