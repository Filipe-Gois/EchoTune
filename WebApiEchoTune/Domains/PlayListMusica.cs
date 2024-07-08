using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEchoTune.Domains
{
    [Table("PlayListMusica")]
    public class PlayListMusica
    {
        [Key]
        public Guid IdPlayListMusica { get; set; } = Guid.NewGuid();

        [ForeignKey("IdMusica")]
        public Musica? Musica { get; set; }
        public Guid IdMusica { get; set; }

        [ForeignKey("IdPlayList")]
        public PlayList? PlayList { get; set; }
        public Guid IdPlayList { get; set; }
    }
}
