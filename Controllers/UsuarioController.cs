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

        [HttpPost]
        [Route("v1/usuarios/")]
        public ObjectResult Post([FromBody] UsuarioCadastroViewModel usuarioTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar usuário.",
                    ModelState.ListarErros());
            }
            var usuario = new Usuario() {
                Id = 0,
                Nome = usuarioTemp.Nome,
                Email = usuarioTemp.Email,
                Senha = usuarioTemp.Senha
            };
            _usuarioRepository.Criar(usuario);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Usuário cadastrado com sucesso!", usuario);
        }

        [HttpPut]
        [Route("v1/usuarios/{id}")]
        public ObjectResult Put(int id, [FromBody] UsuarioEdicaoViewModel usuarioTemp) {
            if (id != usuarioTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id do usuário.");
            }
            
            if (!_usuarioRepository.Existe(usuarioTemp.Id)) {
                ModelState.AddModelError("UsuarioId", "Usuário inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar usuário.",
                    ModelState.ListarErros());
            }
            var usuario = new Usuario() {
                Id = usuarioTemp.Id,
                Nome = usuarioTemp.Nome,
                Email = usuarioTemp.Email,
                Senha = usuarioTemp.Senha
            };
            _usuarioRepository.Editar(usuario);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Usuário editado com sucesso!", usuario);
        }

        [HttpDelete]
        [Route("v1/usuarios/{id}")]
        public ObjectResult Delete(int id) {
            var usuario = _usuarioRepository.Buscar(id);
            if (usuario == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult( "Usuário inexistente.", null);
            }
            try {
                _usuarioRepository.Remover(usuario);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Usuário excluído com sucesso!", usuario);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir o usuário, contate o suporte!", usuario);
            }
        }
    }
}