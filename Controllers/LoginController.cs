using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils;
using Tickets_API.Models.ViewModels;
using Tickets_API.Models.ViewModels.UsuarioViewModels;
using Tickets_API.Repositories;
using Tickets_API.Services;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Efetuar login.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Email": "credencial@admin.com",
        ///        "Senha": "suasenha"
        ///     }
        ///
        /// </remarks>
        /// <returns>Efetua login na API.</returns>
        /// <response code="200">Login realizado com sucesso.</response>  
        /// <response code="404">Usuário e/ou senha inválidos.</response>  
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<UsuarioSimplificadoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status404NotFound)]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioSimplificadoViewModel model) {
            var usuario = UsuarioRepository.Get(model.Email, model.Senha);

            if (usuario == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Usuário ou senha inválidos.", usuario);
            }

            var token = TokenService.GerarToken(usuario);
            usuario.Senha = string.Empty;
            return new {
                usuario = usuario,
                token = token
            };
        }

        /// <summary>
        /// Get deslogado para verificar funcionamento do login.
        /// </summary>
        /// <returns>Retorna informação de visitante.</returns>
        /// <response code="200">Saudações ao visitante.</response>  
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("v1/visitante")]
        [AllowAnonymous]
        public string Visitante() => "Seja bem-vindo visitante";

        /// <summary>
        /// Get logado e admin para verificar autentição como administrador.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        HEADER
        ///
        ///        Key: Authorization
        ///        Value: Bearer + hash
        ///     }
        ///
        /// </remarks>
        /// <returns>Retorna informação de administrador.</returns>
        /// <response code="200">Permissão verificada com sucesso.</response>  
        /// <response code="401">Não possui permissão admin.</response>  
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<UsuarioSimplificadoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("v1/admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Seja bem-vindo administrador!";
    }
}