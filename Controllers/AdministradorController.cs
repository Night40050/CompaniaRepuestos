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
    [Authorize(Policy = "Administrador")]
    public class AdministradorController : Controller
    {
        private readonly CompaniaRepuestosContext _context;
        private readonly ILogger<HomeController> _logger;

        public AdministradorController(CompaniaRepuestosContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult CrearUsuario()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Usuario.Add(usuario);
                    _context.SaveChanges();
                    var auditoria = CrearRegistroAuditoria("Crear Usuario");
                    _context.RegistroAuditoria.Add(auditoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index"); // Redirige a la página principal después de crear el usuario
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario: " + ex.Message);
                }
            }

            // Si hay errores de validación o si la creación del usuario falla, regresa a la vista de creación de usuario
            // para que el usuario pueda corregir los errores y volver a intentarlo
            ViewBag.idRol = new SelectList(_context.Roles, "idRol", "nombreRol", usuario.idRol);
            return View(usuario);
        }
        public IActionResult EditarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = _context.Usuario.Find(usuario.idUsuario);

                    if (usuarioExistente == null)
                    {
                        return NotFound(); // El usuario no fue encontrado en la base de datos
                    }

                  //Aplicar los cambios al objeto del usuario existente
                    usuarioExistente.nombreUsuario = usuario.nombreUsuario;
                    usuarioExistente.Correo = usuario.Correo;
                    usuarioExistente.contraseña = usuario.contraseña;
                    usuarioExistente.idRol = usuario.idRol;

                    //  Guardar los cambios en la base de datos
                    _context.SaveChanges();
                    _context.Update(usuario);
                    var auditoria = CrearRegistroAuditoria("Edito Usuario");
                    _context.RegistroAuditoria.Add(auditoria);
                    _context.SaveChanges();

                    return RedirectToAction("Index"); // Redirecciona a la página principal después de editar
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al editar el usuario: " + ex.Message);
                }
            }

            // Si llegamos aquí, significa que ha ocurrido un error, volvemos a mostrar el formulario de edición con los errores
            return View(usuario);
        }

        public IActionResult EliminarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EliminarUsuario(int idUsuario)
        {
            try
            {
                // Buscar el usuario por su ID en la base de datos
                var usuario = _context.Usuario.Find(idUsuario);
                var auditoria = CrearRegistroAuditoria("Elimino Usuario");
                _context.RegistroAuditoria.Add(auditoria);
                _context.SaveChanges();
                // Verificar si se encontró el usuario
                if (usuario != null)
                {
                    // Eliminar el usuario de la base de datos
                    _context.Usuario.Remove(usuario);
                    _context.SaveChanges();
                    _context.SaveChanges();

                    // Redirigir a la acción Index 
                    return RedirectToAction("Index");
                }
                else
                {
                    // Si no se encontró el usuario, mostrar un mensaje de error o redirigir a una página de error
                    return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home"); 
            }
        }
        public IActionResult CrearProveedor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearProveedor(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Agregar el proveedor a la base de datos
                    _context.Proveedor.Add(proveedor);
                    await _context.SaveChangesAsync();
                    var auditoria = CrearRegistroAuditoria("Crear Proveedor");
                    _context.RegistroAuditoria.Add(auditoria);
                    _context.SaveChanges();
                    // Redirigir a alguna página de confirmación o a la lista de proveedores
                    return RedirectToAction("Index", "Proveedor");
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error que ocurra al agregar el proveedor
                    ModelState.AddModelError("", "Error al crear el proveedor: " + ex.Message);
                }
            }

            // Si hay errores de validación, regresar a la vista de creación con los mensajes de error
            return View(proveedor);
        }
        public IActionResult CrearProducto()
        {
            var viewModel = new CrearProductoViewModel
            {
                Producto = new Producto()

            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto(CrearProductoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Crea el producto
                _context.Producto.Add(viewModel.Producto);
                await _context.SaveChangesAsync();
                var auditoria = CrearRegistroAuditoria("Creo Producto");
                _context.RegistroAuditoria.Add(auditoria);
                _context.SaveChanges();
                // Crea la relación con el proveedor
                var relacion = new RelacionProducProv
                {
                    idProducto = viewModel.Producto.idProducto,
                    idProveedor = viewModel.idProveedor,
                    precioUnitario = viewModel.precioUnitario
                };
                _context.RelacionProducProv.Add(relacion);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Auditoria()
        {
            var registrosAuditoria = _context.RegistroAuditoria.Include(ra => ra.Usuario).ToList();
            return View(registrosAuditoria);
        }
    }
}
