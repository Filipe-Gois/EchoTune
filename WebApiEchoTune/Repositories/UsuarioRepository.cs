using API_FitTrack.Utils;
using Microsoft.EntityFrameworkCore;
using WebAPI.Utils.BlobStorage;
using WebAPI.ViewModels;
using WebApiEchoTune.Contexts;
using WebApiEchoTune.Domains;
using WebApiEchoTune.Interfaces;
using WebApiEchoTune.ViewModels.Usuario;

namespace WebApiEchoTune.Repositories
{
    public class UsuarioRepository(EchoTuneContext echoTuneContext) : IUsuarioRepository
    {
        private readonly EchoTuneContext ctx = echoTuneContext;

        public bool AlterarSenha(string email, string senhaNova)
        {
            Usuario usuarioBuscado = ctx.Usuario.FirstOrDefault(x => x.Email == email) ?? throw new Exception("Usuário não encontrado!");

            usuarioBuscado.Senha = Criptografia.GerarHash(senhaNova);

            ctx.Usuario.Update(usuarioBuscado);

            ctx.SaveChanges();

            return true;
        }

        public void AtualizarDadosPerfil(Guid id, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarFoto(Guid id, Usuario user)
        {
            try
            {
                Usuario usuarioBuscado = ctx.Usuario.Include(x => x.UsuarioMidia).FirstOrDefault(x => x.IdUsuario == id)! ?? throw new Exception("Usuário não encontrado!");

                if (usuarioBuscado.UsuarioMidia!.BlobName != null)
                {
                    await AzureBlobStorageHelper.DeleteBlobAsync(usuarioBuscado.UsuarioMidia!.BlobName);
                }

                usuarioBuscado.UsuarioMidia!.Foto = user.UsuarioMidia!.Foto;
                usuarioBuscado.UsuarioMidia!.BlobName = user.UsuarioMidia!.BlobName;

                ctx.Usuario.Update(usuarioBuscado!);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorEmailEGoogleId(string email, string idGoogleAccount)
        {
            return ctx.Usuario.Include(x => x.UsuarioMidia).Include(x => x.TipoUsuario).Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                //Senha = u.Senha,
                IdGoogleAccount = u.IdGoogleAccount,

                TipoUsuario = new TipoUsuario
                {
                    IdTipoUsuario = u.TipoUsuario!.IdTipoUsuario,
                    Titulo = u.TipoUsuario.Titulo
                },

                UsuarioMidia = new UsuarioMidia
                {
                    IdUsuarioMidia = u.UsuarioMidia!.IdUsuarioMidia,
                    Foto = u.UsuarioMidia.Foto
                },
            }).FirstOrDefault(x => x.Email == email && x.IdGoogleAccount == idGoogleAccount)! ?? throw new Exception("Usuário não encontrado!");
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {

            //retorna null se nao achar o usuario
            Usuario user = ctx.Usuario.Include(x => x.UsuarioMidia).Include(x => x.TipoUsuario).Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                Email = u.Email,
                Senha = u.Senha,
                Nome = u.Nome,

                TipoUsuario = new TipoUsuario
                {
                    IdTipoUsuario = u.TipoUsuario!.IdTipoUsuario,
                    Titulo = u.TipoUsuario.Titulo
                },

                UsuarioMidia = new UsuarioMidia
                {
                    IdUsuarioMidia = u.UsuarioMidia!.IdUsuarioMidia,
                    Foto = u.UsuarioMidia!.Foto
                }
            }).FirstOrDefault
            (x => x.Email == email) ?? throw new Exception("Usuário não encontrado!");


            if (!Criptografia.CompararHash(senha, user.Senha!)) throw new Exception("Usuário não encontrado!");

            return user;

        }

        public Usuario BuscarPorId(Guid id)
        {
            return ctx.Usuario.Include(x => x.UsuarioMidia).Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                Email = u.Email,
                //Senha = u.Senha,
                Nome = u.Nome,

                UsuarioMidia = new UsuarioMidia
                {
                    IdUsuarioMidia = u.UsuarioMidia!.IdUsuarioMidia,
                    Foto = u.UsuarioMidia!.Foto
                }
            }).FirstOrDefault(x => x.IdUsuario == id) ?? throw new Exception();
        }

        public async Task Cadastrar(CadastrarUsuario usuario, bool isCreateAccountGoogle = false, bool isArtista = false)
        {
            Usuario usuarioBuscado = ctx.Usuario.FirstOrDefault(x => x.Email == usuario.Email)!;

            if (usuarioBuscado != null)
            {
                throw new Exception("Já existe um usuário com esse email!");
            }

            if (!isCreateAccountGoogle && usuario.Senha != null)
            {
                usuario.Senha = Criptografia.GerarHash(usuario.Senha!);
            }

            if (usuario.Senha != null && usuario.IdGoogleAccount != null)
            {
                throw new Exception("Não é possível cadastrar uma conta google com senha!");
            }

            if (usuario.Senha == null && usuario.IdGoogleAccount == null)
            {
                throw new Exception("Informe uma senha ou um Google id!");
            }

            if (usuario.Senha == null && !isCreateAccountGoogle)
            {
                throw new Exception("Cadastre uma senha!");
            }

            if (usuario.IdGoogleAccount == null && isCreateAccountGoogle)
            {
                throw new Exception("Cadastre um id google!");
            }

            if (isCreateAccountGoogle && usuario.FotoUri == null)
            {
                throw new Exception("Informe uma uri!");
            }

            string tipoUsuario = isArtista ? "Artista" : "Comum";

            TipoUsuario tipoUsuarioBuscado = ctx.TipoUsuario.FirstOrDefault(x => x.Titulo == tipoUsuario)! ?? throw new Exception("Tipo de usuário não encontrado!");

            Usuario novoUsuario = new()
            {
                Email = usuario.Email,
                IdGoogleAccount = usuario.IdGoogleAccount,
                Senha = usuario.Senha,
                Nome = usuario.Nome,
                IdTipoUsuario = tipoUsuarioBuscado.IdTipoUsuario,
                UsuarioMidia = new()
            };

            if (!isCreateAccountGoogle)
            {
                novoUsuario.UsuarioMidia.Foto = "";
                novoUsuario.UsuarioMidia = await AzureBlobStorageHelper.UploadImageBlobAsync(null!);
            }
            else
            {
                novoUsuario.UsuarioMidia.Foto = usuario.FotoUri;
                novoUsuario.UsuarioMidia.BlobName = "ProfilePictureGoogle" + novoUsuario.IdUsuario.ToString().Replace("-", "");
            }

            novoUsuario.UsuarioMidia.IdUsuarioMidia = novoUsuario.IdUsuario;

            ctx.UsuarioMidia.Add(novoUsuario.UsuarioMidia);
            ctx.Usuario.Add(novoUsuario);
            ctx.SaveChanges();
        }
    }
}
