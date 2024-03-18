using CompaniaRepuestos.Models;
using CompaniaRepuestos.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

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
            this._servicioCR = new ServiciosCompaniaRepuestos(_context);
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //controlador para el inicio de sesion para devolver la vista correspondiente
        [HttpPost]
        public IActionResult Iniciar_Sesion(string correo, string contrasena, string recordar)
        {
            string error = null;
            List<Claim> userClaims = null;
            try
            {
                if (_servicioCR.IsUserLogin(correo, contrasena))
                {
                    Usuario us = _context.Usuario.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo);
                    userClaims = CreateClaims(us.nombreUsuario, us.idUsuario.ToString(), us.Rol.nombreRol);
                }
                else
                {
                    error = "Correo o contraseña incorrectos";
                    return RedirectToAction("Login", new { error = error });
                }
                var identity = new ClaimsIdentity(userClaims, "CompaniaRepuestosAutenticacion");
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
                error = "Error en el inicio de sesión";
                return RedirectToAction("Login", new { error = error });
            }
        }
        private List<Claim> CreateClaims(string nombre,string id, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,nombre),
                    new Claim(ClaimTypes.Actor,id),
                    new Claim(ClaimTypes.Role, role),
                };

            return claims;
        }

    }
}