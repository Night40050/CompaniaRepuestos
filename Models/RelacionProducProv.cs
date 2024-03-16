using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class RelacionProducProv //modelo para la tabla que relaciona las tablas proveedor y producto DB
    {
        [Key]
        public int idRelacion { get; set; }
        [Required]
        public double precioUnitario { get; set; }
        [ForeignKey("Producto")]
        public int idProducto { get; set; }
        public virtual Producto? Producto { get; set; }
        [ForeignKey("Proveedor")]
        public int idProveedor { get; set; }
        public virtual Proveedor? Proveedor { get; set; }
    }
}
