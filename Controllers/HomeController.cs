using CitaFacil.Models;
using CompaniaRepuestos.Data;
using CompaniaRepuestos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CompaniaRepuestos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompaniaRepuestosContext _context;
        private readonly ServiciosCompaniaRepuestos _servicioCR;

        public HomeController(ILogger<HomeController> logger, CompaniaRepuestosContext context)
        {
            _logger = logger;
            this._context = context;
            this._servicioCF = new ServiciosCitaFacil(_context);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string rol = User.FindFirst(ClaimTypes.Role).Value.ToString();
                if (rol.Equals("Usuarios"))
                {
                    return RedirectToAction("Index", "Usuarios");
                }
                if (rol.Equals("Administrador"))
                {
                    return RedirectToAction("Index", "Administrador");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //controlador para el inicio de sesion para devolver la vista correspondiente
        public IActionResult Iniciar_Sesion(string correo, string contrasena, string recordar)
        {
            string error = null;
            List<Claim> userClaims = null;

            try
            {
                if (_servicioCF.IsUserLogin(correo, contrasena))//Es usuario normal
                {
                    Usuario us = _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo);
                    userClaims = CreateClaims(us.Id_Usuario.ToString(), us.Id_EstadoUsuario.ToString(), us.Rol.Nombre_Rol);

                }
                

                var identity = new ClaimsIdentity(userClaims, "CitaFacilAutenticacion");
                var principal = new ClaimsPrincipal(identity);
                bool recordarU = false;
                if (!string.IsNullOrEmpty(recordar))
                {
                    recordarU = true;
                }
                var authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = recordarU // Establece IsPersistent en true para recordar la sesión
                };

                HttpContext.SignInAsync("CompaniaRepuestosCookie", principal, authenticationProperties);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = error });
            }

        }

    }
}