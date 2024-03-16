using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class Proveedor //modelo para la tabla proveedor DB
    {
        [Key]
        public int idProveedor { get; set; }

        [Required, MinLength(3), StringLength(100)]
        public string nombreProveedor { get; set; }
        [Required]
        public int telefono { get; set; }
        [Required, StringLength(50)]
        public string correo { get; set; }
        [Required, StringLength(50)]
        public string direccion { get; set; }
    }
}
