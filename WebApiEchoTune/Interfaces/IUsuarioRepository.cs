using WebApiEchoTune.Domains;

namespace WebApiEchoTune.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario usuario);

        Usuario BuscarPorId(Guid id);

        Usuario BuscarPorEmailESenha(string email, string senha);

        bool AlterarSenha(string email, string senhaNova);

        Task AtualizarFoto(Guid id, Usuario user);

        void AtualizarDadosPerfil(Guid id, Usuario usuario);
    }
}
