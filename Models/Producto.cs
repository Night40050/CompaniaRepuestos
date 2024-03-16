using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class Producto //modelo para la tabla producto DB
    {
        [Key]
        public int idProducto { get; set; }
        [Required]
        public int cantidadProducto { get; set; }
        [Required, StringLength(100)]
        public string descripcion { get; set; }
        [Required, StringLength(100)]
        public string modelo { get; set; }
        [Required]
        public double valorVenta { get; set; }
    }
}
