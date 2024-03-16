using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCompaniaRepuestos.Models
{
    public class Rol //modelo para la tabla rol DB
    {
        [Key]
        public int idRol { get; set; }
        [Required, StringLength(50)]
        public string nombreRol { get; set; }
    }
}
