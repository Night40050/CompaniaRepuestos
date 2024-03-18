using CompaniaRepuestos.Models;

namespace CompaniaRepuestos.ViewModels
{
    public class CrearProductoViewModel
    {
        public Producto Producto { get; set; }
        public int idProveedor { get; set; }
        public double precioUnitario { get; set; }
    }
}
