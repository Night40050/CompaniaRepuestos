using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompaniaRepuestos.Models
{
    public class RegistroAuditoria //modelo para la tabla de auditoria DB
    {
        [Key]
        public int idAuditoria { get; set; }
        [Required]
        public DateTime fechaAccion { get; set; }
        [Required, StringLength(100)]
        public string accionRealizada { get; set; }
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
