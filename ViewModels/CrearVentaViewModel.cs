using CompaniaRepuestos.Models;

namespace CompaniaRepuestos.ViewModels
{
    public class CrearVentaViewModel
    {
        public Venta Venta { get; set; }
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
        public List<Producto> Productos { get; set; } // Lista de productos
    }
}
