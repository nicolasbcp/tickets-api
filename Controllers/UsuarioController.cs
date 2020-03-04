using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class UsuarioController : ControllerBase {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        /// <returns>Exibe a lista de usuários cadastrados.</returns>
        /// <response code="200">Listagem de usuários.</response>  
        /// <response code="404">Usuário não encontrado.</response> 
        [HttpGet]
        [Route("v1/usuarios")]
        public ObjectResult Get() {
            var usuario = _usuarioRepository.Listar();

            if(!usuario.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum usuário encontrado.", usuario);
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de usuários!", usuario);
        }

        /// <summary>
        /// Buscar usuário por ID.
        /// </summary>
        /// <returns>Exibe usuário específico cadastrado por ID.</returns>
        /// <response code="200">Usuário encontrado com sucesso.</response>  
        /// <response code="404">Usuário não encontrado.</response>  
        [HttpGet]
        [Route("v1/usuarios/{id}")]
        public ObjectResult Get(int id) {
            var usuario = _usuarioRepository.Buscar(id);

            if (usuario == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Usuário não encontrado!");
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Usuário encontrado!", usuario.Nome);
        }
    }
}