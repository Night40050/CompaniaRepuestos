using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class Venta //modelo para la tabla ventas DB
    {

        [Key]
        public int idVenta { get; set; }
        [Required]
        public DateTime fechaVenta { get; set; }
        [Required]
        public double totalVenta { get; set; }
        [ForeignKey("Usuarios")]
        public int idUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
