using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompaniaRepuestos.Data;
using CompaniaRepuestos.Models;

namespace CompaniaRepuestos.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly CompaniaRepuestosContext _context;
        public AdministradorController(CompaniaRepuestosContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }
       

    }
}
