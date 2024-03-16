using CompaniaRepuestos.Data;
using Microsoft.EntityFrameworkCore;

namespace CitaFacil.Models
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
                        nombreRol = "Usuarios"
                    }
                 );
                context.SaveChanges();
                context.Usuarios.Add(new Usuario
                {
                    nombreUsuario = "Daniel Caicedo",
                    contraseña = servicios.HashPassword("12345678"),                           
                    Rol = 1,
                    Correo = "sergiomoscoso1022@hotmail.com"
                });
                context.SaveChanges();
            }
        }
    }
}
