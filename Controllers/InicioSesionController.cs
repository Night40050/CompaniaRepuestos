using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompaniaRepuestos.Controllers
{
    public class InicioSesionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(string correo, string contrasena)
        {
            // Lógica de autenticación
            return RedirectToAction("Index", "Home");
        }
    }

    

    [Authorize(Policy = "Usuario")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}

