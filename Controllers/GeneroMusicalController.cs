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

        /// <summary>
        /// Listar todas os gêneros musicais.
        /// </summary>
        /// <returns>Exibe a lista de gêneros musicais cadastrados.</returns>
        /// <response code="200">Listagem de gêneros musicais.</response>  
        /// <response code="404">Gênero musical não encontrado.</response>  
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

        /// <summary>
        /// Buscar gênero musical por ID.
        /// </summary>
        /// <returns>Exibe gênero musical específico cadastrado por ID.</returns>
        /// <response code="200">Gênero musical encontrado com sucesso.</response>  
        /// <response code="404">Gênero musical não encontrado.</response>  
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

        /// <summary>
        /// Cadastrar gênero musical.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Nome": "Rock",
        ///     }
        ///
        /// </remarks>
        /// <returns>Cadastra um novo gênero musical.</returns>
        /// <response code="201">Gênero musical criado com sucesso.</response>  
        /// <response code="400">Erro ao cadastrar gênero musical.</response>  
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<GeneroMusical>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
        [Route("v1/generosmusicais/")]
        public ObjectResult Post([FromBody] GeneroMusicalSimplificadoViewModel generoMusicalTemp) {
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

        /// <summary>
        /// Edita um gênero musical.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        	"Id": 1,
        ///         "Nome": "Rock - Atualizado",
        ///     }
        ///
        /// </remarks>    
        /// <returns>Edita um gênero musical especificado por ID.</returns>
        /// <response code="200">Gênero musical editado com sucesso.</response>
        /// <response code="400">Erro ao editar gênero musical.</response>  
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<GeneroMusical>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
        [Route("v1/generosmusicais/{id}")]
        public ObjectResult Put(int id, [FromBody] GeneroMusicalSimplificadoViewModel generoMusicalTemp) {
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

        /// <summary>
        /// Deleta um gênero musical.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <returns>Deleta um gênero musical.</returns>
        /// <response code="404">Gênero musical não localizado.</response>  
        /// <response code="406">Relação não permitida para exclusão.</response>  
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