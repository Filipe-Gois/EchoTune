namespace WebApiEchoTune.Interfaces
{
    public interface IPlaylistRepository
    {
        void AdicionarMusica(Guid idMusica);
        void RemoverMusica(Guid idMusica);

    }
}
