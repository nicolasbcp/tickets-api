using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Models;
using Tickets_API.Repositories;
using Tickets_API.Services;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model) {
            var usuario = UsuarioRepository.Get(model.Email, model.Senha);

            if (usuario == null) {
                return NotFound(new {message = "Usuário ou senha inválidos"});
            }

            var token = TokenService.GerarToken(usuario);
            usuario.Senha = "";
            return new {
                usuario = usuario,
                token = token
            };
        }

        [HttpGet]
        [Route("v1/anonimo")]
        [AllowAnonymous]
        public string Anonimo() => "Anônimo";

        [HttpGet]
        [Route("v1/autenticado")]
        [Authorize]
        public string Autenticado() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("v1/admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Admin";
    }
}