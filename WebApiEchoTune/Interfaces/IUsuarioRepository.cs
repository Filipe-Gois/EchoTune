using WebAPI.ViewModels;
using WebApiEchoTune.Domains;
using WebApiEchoTune.ViewModels.Usuario;

namespace WebApiEchoTune.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Cadastrar(CadastrarUsuario usuario, bool isCreateAccountGoogle = false, bool isArtista = false);

        Usuario BuscarPorId(Guid id);

        Usuario BuscarPorEmailESenha(string email, string senha);

        Usuario BuscarPorEmailEGoogleId(string email, string idGoogleAccount);

        bool AlterarSenha(string email, string senhaNova);

        Task AtualizarFoto(Guid id, Usuario user);

        void AtualizarDadosPerfil(Guid id, Usuario usuario);
    }
}
