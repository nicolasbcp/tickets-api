using System;
using System.Linq;
using GFT_Podcasts.Libraries.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.GeneroMusicalViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class GeneroMusicalController : ControllerBase
    {
        private readonly IGeneroMusicalRepository _generoMusicalRepository;

        public GeneroMusicalController(IGeneroMusicalRepository generoMusicalRepository) {
            _generoMusicalRepository = generoMusicalRepository;
        }

        [HttpGet]
        [Route("v1/generosmusicais/{id}")]
        public ObjectResult Get(int id) {
            var generoMusical = _generoMusicalRepository.Buscar(id);

            if (generoMusical == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Gênero musical não encontrado!");
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Gênero musical encontrado!", generoMusical);
        }
        [HttpGet]
        [Route("v1/generosmusicais")]
        public ObjectResult Get() {
            var generoMusical = _generoMusicalRepository.Listar();

            if(!generoMusical.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum gênero musical encontrado.", generoMusical);
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de gêneros musicais!", generoMusical);
        }

        [HttpPost]
        [Route("v1/generosmusicais/")]
        public ObjectResult Post([FromBody] GeneroMusicalCadastroViewModel generoMusicalTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar gênero musical.",
                    ModelState.ListarErros());
            }
            var generoMusical = new GeneroMusical() {
                Id = 0,
                Nome = generoMusicalTemp.Nome,
            };
            _generoMusicalRepository.Criar(generoMusical);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Gênero Musical cadastrado com sucesso!", generoMusical);
        }

        [HttpPut]
        [Route("v1/generosmusicais/{id}")]
        public ObjectResult Put(int id, [FromBody] GeneroMusicalEdicaoViewModel generoMusicalTemp) {
            if (id != generoMusicalTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id do gênero musical.");
            }
            
            if (!_generoMusicalRepository.Existe(generoMusicalTemp.Id)) {
                ModelState.AddModelError("GeneroMusicalId", "Gênero musical inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar gênero musical.",
                    ModelState.ListarErros());
            }
            var generoMusical = new GeneroMusical() {
                Id = generoMusicalTemp.Id,
                Nome = generoMusicalTemp.Nome
            };
            _generoMusicalRepository.Editar(generoMusical);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Gênero musical editado com sucesso!", generoMusical);
        }

        [HttpDelete]
        [Route("v1/generosmusicais/{id}")]
        public ObjectResult Delete(int id) {
            var generoMusical = _generoMusicalRepository.Buscar(id);
            if (generoMusical == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult( "Gênero musical inexistente.", null);
            }
            try {
                _generoMusicalRepository.Remover(generoMusical);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Gênero musical excluído com sucesso!", generoMusical);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir o gênero musical, contate o suporte!",
                 generoMusical);
            }
        }
    }
}