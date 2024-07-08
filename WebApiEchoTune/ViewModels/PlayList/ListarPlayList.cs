using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiEchoTune.Domains;
using WebApiEchoTune.ViewModels.Musica;

namespace WebApiEchoTune.ViewModels.PlayList
{
    public class ListarPlayList
    {
        public Guid IdPlaylist { get; set; }

        public string? Nome { get; set; }

        public string? Tema { get; set; }

        public Guid IdUsuario { get; set; }
        public List<ListarMusica>? Musicas { get; set; }
    }
}
