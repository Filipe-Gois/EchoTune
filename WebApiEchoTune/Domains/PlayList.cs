using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEchoTune.Domains
{
    [Table("PlayList")]
    public class PlayList
    {
        [Key]
        public Guid IdPlaylist { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "Informe o nome da playlist!")]
        public string? Nome { get; set; }

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "Informe o tema da playlist!")]
        public string? Tema { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
