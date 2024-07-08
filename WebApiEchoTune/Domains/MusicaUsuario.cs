using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEchoTune.Domains
{
    [Table("MusicaUsuario")]
    public class MusicaUsuario
    {
        [Key]
        public Guid IdMusicaUsuario { get; set; } = Guid.NewGuid();

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        public Guid IdUsuario { get; set; }


        [ForeignKey("IdMusica")]
        public Musica? Musica { get; set; }
        public Guid IdMusica { get; set; }

        [Column(TypeName = "BIT")]
        public bool Favoritada { get; set; } = false;
    }
}
