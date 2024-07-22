using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiEchoTune.Domains;

namespace WebApiEchoTune.ViewModels.Usuario
{
    public class CadastrarUsuario
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? IdGoogleAccount { get; set; }

        public string? FotoUri { get; set; }

    }
}
