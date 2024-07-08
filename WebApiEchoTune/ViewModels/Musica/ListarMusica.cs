using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiEchoTune.ViewModels.Musica
{
    public class ListarMusica
    {
        public Guid IdMusica { get; set; }
        public string? Nome { get; set; }
        public TimeOnly Duracao { get; set; }
        public string? NomeArtista { get; set; }
        public string? NomeAlbum { get; set; }

        public bool Favoritada { get; set; }
    }
}
