using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class DetalleVenta //modelo para la tabla de detalles de ventas DB
    {
        [Key]
        public int idDetalle { get; set; }
        [Required]
        public int cantidadVendida { get; set; }
        [ForeignKey("Venta")]
        public int idVenta { get; set; }
        public virtual Venta? Venta { get; set; }
        [ForeignKey("Producto")]
        public int idProducto { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
