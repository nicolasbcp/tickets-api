using System;
using System.Linq;
using GFT_Podcasts.Libraries.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.UsuarioViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class UsuarioController : ControllerBase {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("v1/usuarios/{id}")]
        public ObjectResult Get(int id) {
            var usuario = _usuarioRepository.Buscar(id);

            if (usuario == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Usuário não encontrado!");
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Usuário encontrado!", usuario);
        }
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
    }
}