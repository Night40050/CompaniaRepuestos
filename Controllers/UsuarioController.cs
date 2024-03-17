using Microsoft.AspNetCore.Mvc;

namespace CompaniaRepuestos.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
