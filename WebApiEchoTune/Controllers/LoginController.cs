using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApiEchoTune.Domains;
using WebApiEchoTune.Interfaces;
using WebApiEchoTune.Repositories;
using WebApiEchoTune.ViewModels.Login;

namespace WebApiEchoTune.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IUsuarioRepository usuarioRepository) : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginViewModel.Email!, loginViewModel.Senha!) ?? throw new Exception("Email ou senha inválidos!");

                Claim[] claims =
                [
                new(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                new(JwtRegisteredClaimNames.Name, usuarioBuscado.Nome!),
                new(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new("Foto", usuarioBuscado.UsuarioMidia!.Foto!)
                ];

                SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes("EchoTune-webapi-chave-symmetricsecuritykey"));

                SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken meuToken = new(
                        issuer: "API-EchoTune",
                        audience: "API-EchoTune",
                        claims: claims,
                        expires: DateTime.Now.AddDays(7),
                        signingCredentials: creds
                    );

                return StatusCode(200, new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
