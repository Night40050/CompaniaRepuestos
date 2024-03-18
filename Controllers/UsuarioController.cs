using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompaniaRepuestos.Data;
using CompaniaRepuestos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompaniaRepuestos.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CompaniaRepuestos.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly CompaniaRepuestosContext _context;
        private readonly ILogger<HomeController> _logger;

        public UsuarioController(CompaniaRepuestosContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Venta()
        {
            var viewModel = new CrearVentaViewModel
            {
                Venta = new Venta()
            };
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Venta(CrearVentaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Crea la venta
                var userId = 1; // Valor por defecto
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Actor);
                var idproducto = viewModel.Producto.idProducto;
                var fechaActual = DateTime.Now;
                var cantidadDispo = viewModel.Producto.cantidadProducto;
                var valorVenta = 1.0;
                var totalVenta = 1.0;
                var cantidadVendida = viewModel.Venta.cantidadVendida;
                var producto = await _context.Producto.FindAsync(viewModel.Producto.idProducto);
                try
                {
                    if (producto != null)
                    {
                        cantidadDispo = producto.cantidadProducto;
                        valorVenta = producto.valorVenta;
                    }
                    else
                    {
                        // Manejar el caso cuando no se encuentra el producto
                        ModelState.AddModelError(string.Empty, "El producto no fue encontrado.");
                        return View(viewModel);
                    }
                    if (idproducto != null && cantidadVendida <= cantidadDispo)
                    {
                        // Calcular el precio total
                        totalVenta = valorVenta * cantidadVendida;
                        producto.cantidadProducto -= cantidadVendida;
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Si el producto no existe o la cantidad a vender es mayor que la cantidad disponible
                        ModelState.AddModelError(string.Empty, "Error en la venta: Producto no encontrado o cantidad insuficiente.");
                        return View(viewModel); // Volver a la vista de venta con el modelo para corregir la cantidad
                    }
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int parsedUserId))
                    {
                        userId = parsedUserId;
                    }
                    // Crea la relación con el usuario y producto
                    var venta = new Venta
                    {
                        idProducto = idproducto,
                        fechaVenta = fechaActual,
                        totalVenta = totalVenta,
                        cantidadVendida = cantidadVendida,
                        idUsuario = userId
                    };
                    var auditoria = CrearRegistroAuditoria("Crear Venta");
                    _context.RegistroAuditoria.Add(auditoria);
                    _context.Venta.Add(venta);
                    _context.SaveChanges();
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Venta)); // Redirige a la página principal después de crear el usuario
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario: " + ex.Message);
                }

            }
            return View(viewModel);
        }
        
        private RegistroAuditoria CrearRegistroAuditoria(string accionRealizada)
        {
            var userId = 1; // Valor por defecto
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Actor);
            var fechaActual = DateTime.Now;

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int parsedUserId))
            {
                userId = parsedUserId;
            }

            var auditoria = new RegistroAuditoria
            {
                idUsuario = userId,
                fechaAccion = fechaActual,
                accionRealizada = accionRealizada
            };

            return auditoria;
        }
        public IActionResult GenerarInformeDiario()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Today;

            // Obtener las ventas realizadas durante el día actual
            var ventasDelDia = _context.Venta
                .Include(v => v.Producto)
                .Where(v => v.fechaVenta.Date == fechaActual)
                .ToList();

            // Calcular el monto total de ventas realizadas durante el día
            double montoTotalVentas = ventasDelDia.Sum(v => v.totalVenta);

            // Crear un diccionario para almacenar los repuestos vendidos y sus cantidades
            Dictionary<string, int> repuestosVendidos = new Dictionary<string, int>();

            // Iterar sobre las ventas del día para contar la cantidad de cada repuesto vendido
            foreach (var venta in ventasDelDia)
            {
                string descripcionRepuesto = venta.Producto.descripcion;

                if (repuestosVendidos.ContainsKey(descripcionRepuesto))
                {
                    repuestosVendidos[descripcionRepuesto] += venta.cantidadVendida;
                }
                else
                {
                    repuestosVendidos[descripcionRepuesto] = venta.cantidadVendida;
                }
            }

            // Pasar los datos al modelo de vista del informe
            var informeViewModel = new InformeDiarioViewModel
            {
                Fecha = fechaActual,
                MontoTotalVentas = montoTotalVentas,
                RepuestosVendidos = repuestosVendidos
            };

            return View(informeViewModel);
        }
    }
}
