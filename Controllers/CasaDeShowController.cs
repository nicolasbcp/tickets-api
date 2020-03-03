using System;
using System.Linq;
using GFT_Podcasts.Libraries.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.CasaDeShowViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class CasaDeShowController : ControllerBase {
        private readonly ICasaDeShowRepository _casaDeShowRepository;

        public CasaDeShowController(ICasaDeShowRepository casaDeShowRepository) {
            _casaDeShowRepository = casaDeShowRepository;
        }

        [HttpGet]
        [Route("v1/casasdeshow/{id}")]
        public ObjectResult Get(int id) {
            var casadeshow = _casaDeShowRepository.Buscar(id);

            if (casadeshow == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Casa de Show não encontrada!");
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Casa de Show encontrada!", casadeshow);
        }
        [HttpGet]
        [Route("v1/casasdeshow")]
        public ObjectResult Get() {
            var casadeshow = _casaDeShowRepository.Listar();

            if(!casadeshow.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhuma casa de show encontrada.", casadeshow);
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de casas de show!", casadeshow);
        }

        [HttpPost]
        [Route("v1/casasdeshow/")]
        public ObjectResult Post([FromBody] CasaDeShowCadastroViewModel casaDeShowTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar casa de show.",
                    ModelState.ListarErros());
            }
            var casadeshow = new CasaDeShow() {
                Id = 0,
                Nome = casaDeShowTemp.Nome,
                Endereco = casaDeShowTemp.Endereco,
            };
            _casaDeShowRepository.Criar(casadeshow);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Casa de Show cadastrada com sucesso!", casadeshow);
        }

        [HttpPut]
        [Route("v1/casasdeshow/{id}")]
        public ObjectResult Put(int id, [FromBody] CasaDeShowEdicaoViewModel casaDeShowTemp) {
            if (id != casaDeShowTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da casa de show.");
            }
            
            if (!_casaDeShowRepository.Existe(casaDeShowTemp.Id)) {
                ModelState.AddModelError("CasaDeShowId", "Casa de Show inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar casa de show.",
                    ModelState.ListarErros());
            }
            var casadeshow = new CasaDeShow() {
                Id = casaDeShowTemp.Id,
                Nome = casaDeShowTemp.Nome,
                Endereco = casaDeShowTemp.Endereco
            };
            _casaDeShowRepository.Editar(casadeshow);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Casa de Show editada com sucesso!", casadeshow);
        }

        [HttpDelete]
        [Route("v1/casasdeshow/{id}")]
        public ObjectResult Delete(int id) {
            var casadeshow = _casaDeShowRepository.Buscar(id);
            if (casadeshow == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult( "Casa de Show inexistente.", null);
            }
            try {
                _casaDeShowRepository.Remover(casadeshow);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Casa de Show excluída com sucesso!", casadeshow);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir a casa de show, contate o suporte!", casadeshow);
            }
        }
    }
}