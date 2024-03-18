using CompaniaRepuestos.Data;
using Microsoft.EntityFrameworkCore;

namespace CompaniaRepuestos.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CompaniaRepuestosContext(
                serviceProvider.GetRequiredService<DbContextOptions<CompaniaRepuestosContext>>()))
            {
                ServiciosCompaniaRepuestos servicios = new ServiciosCompaniaRepuestos(context);

                if (context.Roles.Any())
                {
                    return;
                }
                context.Roles.AddRange(
                    new Rol
                    {
                        nombreRol = "Administrador"
                    },
                    new Rol
                    {
                        nombreRol = "Usuario"
                    }
                 );
                context.SaveChanges();
                context.Usuario.Add(new Usuario
                {
                    nombreUsuario = "Daniel Caicedo",
                    contraseña = servicios.HashPassword("12345678"),                           
                    idRol = 1,
                    Correo = "danieleduaedo13@gmail.com"
                });
                context.SaveChanges();
                context.Usuario.Add(new Usuario
                {
                    nombreUsuario = "usuario",
                    contraseña = servicios.HashPassword("12345678"),
                    idRol = 2,
                    Correo = "usuario@gmail.com"
                });
                context.SaveChanges();
            }
        }
    }
}
