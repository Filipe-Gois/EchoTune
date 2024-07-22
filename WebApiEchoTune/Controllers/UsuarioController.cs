using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels;
using WebApiEchoTune.Interfaces;
using WebApiEchoTune.ViewModels.Usuario;

namespace WebApiEchoTune.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUsuarioRepository usuarioRepository) : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromForm] CadastrarUsuario usuario, bool isCreateAccountGoogle = false, bool isArtista = false)
        {
            try
            {
                await _usuarioRepository.Cadastrar(usuario, isCreateAccountGoogle, isArtista);
                return StatusCode(201);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
