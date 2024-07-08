using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEchoTune.Domains
{
    [Table("Musica")]
    public class Musica
    {
        [Key]
        public Guid IdMusica { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "O Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Column(TypeName = "TIME")]
        [Required(ErrorMessage = "A duração é obrigatória!")]
        public TimeOnly Duracao { get; set; }

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "O nome do artista é obrigatório!")]
        public string? NomeArtista { get; set; }

        [Column(TypeName = "VARCHAR(60)")]
        [Required(ErrorMessage = "O album do artista é obrigatório!")]
        public string? NomeAlbum { get; set; }

    }
}
