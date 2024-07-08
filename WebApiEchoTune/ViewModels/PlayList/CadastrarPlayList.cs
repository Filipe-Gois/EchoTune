using WebApiEchoTune.ViewModels.Musica;

namespace WebApiEchoTune.ViewModels.PlayList
{
    public class CadastrarPlayList
    {
        public string? Nome { get; set; }
        public string? Tema { get; set; }
        public Guid IdUsuario { get; set; }
        public List<Guid>? IdMusicas { get; set; }
    }
}
