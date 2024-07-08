using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEchoTune.Domains
{
    [Table("UsuarioMidia")]
    public class UsuarioMidia
    {
        [Key]
        public Guid IdUsuarioMidia { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(60)")]
        public string? BlobName { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Foto { get; set; }
    }
}
