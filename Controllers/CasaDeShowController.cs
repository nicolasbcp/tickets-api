using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels;
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

        /// <summary>
        /// Listar todas as casas de show.
        /// </summary>
        /// <returns>Exibe a lista de casas de show cadastradas.</returns>
        /// <response code="200">Listagem de casas de show.</response>  
        /// <response code="404">Casa de show não encontrada.</response>  
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

        /// <summary>
        /// Listar as casas de show em ordem alfabética crescente por nome.
        /// </summary>
        /// <returns>Exibe a lista de casas de show cadastradas.</returns>
        /// <response code="200">Listagem de casas de show.</response>  
        /// <response code="404">Não há casas de show cadastradas.</response>  
        [HttpGet]
        [Route("v1/casasdeshow/asc")]
        public ObjectResult GetAsc() {
            var casadeshow = _casaDeShowRepository.Listar();

            if(!casadeshow.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Não há casas de show cadastradas.Listagem de casas de show.", casadeshow);
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de casas de show.", casadeshow.OrderBy(s => s.Nome));
        }

        /// <summary>
        /// Listar as casas de show em ordem alfabética decrescente por nome.
        /// </summary>
        /// <returns>Exibe a lista de casas de show cadastradas.</returns>
        /// <response code="200">Listagem de casas de show.</response>  
        /// <response code="404">Não há casas de show cadastradas.</response>  
        [HttpGet]
        [Route("v1/casasdeshow/desc")]
        public ObjectResult GetDesc() {
            var casadeshow = _casaDeShowRepository.Listar();

            if(!casadeshow.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhuma casa de show encontrada.", casadeshow);
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de casas de show!", casadeshow.OrderByDescending(s => s.Nome));
        }

        /// <summary>
        /// Buscar casa de show por ID.
        /// </summary>
        /// <returns>Exibe casa de show específica cadastrada por ID.</returns>
        /// <response code="200">Casa de show encontrada com sucesso.</response>  
        /// <response code="404">Casa de show não encontrada.</response>  
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

        /// <summary>
        /// Buscar casa de show por nome.
        /// </summary>
        /// <returns>Exibe casa de show específica cadastrada por nome.</returns>
        /// <response code="200">Casa de show encontrada com sucesso.</response>  
        /// <response code="404">Casa de show não encontrada.</response>  
        [HttpGet]
        [Route("v1/casasdeshow/nome/{nome}")]
        public ObjectResult GetNome(string nome) {
            var casadeshow = _casaDeShowRepository.BuscarNome(nome);

            if (casadeshow == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Casa de Show não encontrada!");
            }
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Casa de Show encontrada!", casadeshow);
        }

        /// <summary>
        /// Cadastrar casa de show.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Nome": "Live Curitiba",
        ///        "Endereco": "R. Itajubá, n. 200",
        ///     }
        ///
        /// </remarks>
        /// <returns>Cadastra uma nova casa de show.</returns>
        /// <response code="201">Casa de show criada com sucesso.</response>  
        /// <response code="400">Erro ao cadastrar casa de show.</response>  
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<CasaDeShow>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Edita uma casa de show.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        	"Id": 1,
        ///         "Nome": "Live Curitiba - Atualizada",
        ///         "Endereco": "R. Itajubá, n. 200",
        ///     }
        ///
        /// </remarks>    
        /// <returns>Edita uma casa de show especificada por ID.</returns>
        /// <response code="200">Casa de show editada com sucesso.</response>
        /// <response code="400">Erro ao editar casa de show.</response>  
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<CasaDeShow>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Deleta uma casa de show.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <returns>Deleta uma casa de show.</returns>
        /// <response code="404">Casa de show não localizada.</response>  
        /// <response code="406">Relação não permitida para exclusão.</response>  
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