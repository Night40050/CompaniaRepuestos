using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompaniaRepuestos.Models
{
    public class Venta //modelo para la tabla ventas DB
    {

        [Key]
        public int idVenta { get; set; }
        [Required]
        public DateTime fechaVenta { get; set; }
        [Required]
        public double totalVenta { get; set; }
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }
        [ForeignKey("Producto")]
        public int idProducto { get; set; }
        public virtual Producto? Producto { get; set; }
        [Required]
        public int cantidadVendida { get; set; }
    }
}
