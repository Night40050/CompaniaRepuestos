using Microsoft.EntityFrameworkCore;
using CompaniaRepuestos.Models;

namespace CompaniaRepuestos.Data
//representa el contexto para la base de datos, provee acceso a la base de datos y habilita operaciones para las entidades 
{
    public class CompaniaRepuestosContext : DbContext   
    {
        // Initializes a new instance of the CompaniaRepuestosContext with the specified options.
        public CompaniaRepuestosContext(DbContextOptions<CompaniaRepuestosContext> options) : base(options) { }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<RegistroAuditoria> RegistroAuditoria { get; set; }
        public DbSet<RelacionProducProv> RelacionProducProv { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venta> Venta { get; set; }

        //Dependiendo de la version de Entity Framework puede en ocasiones pluralizar las tablas cuando se mapean los modelos, de ser asi use esta seccion cambiando el nombre de la tabla pluralizada "es"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Rol>().ToTable("Rol");
        }
        
    }
}
