using WebApiEchoTune.Domains;
using WebApiEchoTune.Interfaces;

namespace WebApiEchoTune.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool AlterarSenha(string email, string senhaNova)
        {
            throw new NotImplementedException();
        }

        public void AtualizarDadosPerfil(Guid id, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarFoto(Guid id, Usuario user)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
