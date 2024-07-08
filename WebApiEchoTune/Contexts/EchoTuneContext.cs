using Microsoft.EntityFrameworkCore;
using WebApiEchoTune.Domains;

namespace WebApiEchoTune.Contexts
{
    public class EchoTuneContext(DbContextOptions<EchoTuneContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Musica> Musica { get; set; }
        public DbSet<PlayList> PlayList { get; set; }
        public DbSet<PlayListMusica> PlaylistMusica { get; set; }
        public DbSet<MusicaUsuario> MusicaUsuario { get; set; }
        public DbSet<UsuarioMidia> UsuarioMidia { get; set; }
    }
}
