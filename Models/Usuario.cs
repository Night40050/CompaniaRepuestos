using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CompaniaRepuestos.Models
{
    public class Usuario //modelo para la tabla usuario DB
    {
        [Key]
        public int idUsuario { get; set; }

        [Required, MinLength(3), StringLength(100)]
        public string nombreUsuario { get; set; }

        [Required]
        public string Correo { get; set; }       

        [Required, MinLength(8), Column(TypeName = "nvarchar(MAX)")]
        public string contraseña { get; set; }

        [ForeignKey("Roles")]
        public int idRol { get; set; }
        public virtual Rol? Rol { get; set; }
    }
}
