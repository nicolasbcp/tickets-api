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
using Tickets_API.Models.ViewModels.EventoViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{

    [Route("api/")]
    public class EventoController : ControllerBase {
        private readonly IEventoRepository _eventoRepository;
        private readonly ICasaDeShowRepository _casaDeShowRepository;
        private readonly IGeneroMusicalRepository _generoMusicalRepository;
        
        public EventoController(IEventoRepository eventoRepository, 
            ICasaDeShowRepository casaDeShowRepository, IGeneroMusicalRepository generoMusicalRepository) {
                _eventoRepository = eventoRepository;
                _casaDeShowRepository = casaDeShowRepository;
                _generoMusicalRepository = generoMusicalRepository;
        }

        /// <summary>
        /// Listar todos os eventos.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos")]
        public ObjectResult Get() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos);
        }

        /// <summary>
        /// Listar eventos em ordem crescente por capacidade.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/capacidade/asc")]
        public ObjectResult GetCapacidadeAsc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderBy(s => s.Capacidade));
        }

        /// <summary>
        /// Listar eventos em ordem decrescente por capacidade.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/capacidade/desc")]
        public ObjectResult GetCapacidadeDesc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderByDescending(s => s.Capacidade));
        }

        /// <summary>
        /// Listar eventos em ordem crescente por data.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/data/asc")]
        public ObjectResult GetDataAsc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderBy(s => s.Data));
        }

        /// <summary>
        /// Listar eventos em ordem decrescente por data.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/data/desc")]
        public ObjectResult GetDataDesc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderByDescending(s => s.Data));
        }

        /// <summary>
        /// Listar eventos em ordem alfabética crescente por nome.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/nome/asc")]
        public ObjectResult GetNomeAsc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderBy(s => s.Nome));
        }

        /// <summary>
        /// Listar eventos em ordem alfabética decrescente por nome.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/nome/desc")]
        public ObjectResult GetNomeDesc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderByDescending(s => s.Nome));
        }

        /// <summary>
        /// Listar eventos em ordem crescente por valor unitário.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/valorunitario/asc")]
        public ObjectResult GetValorAsc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderBy(s => s.ValorUnitario));
        }

        /// <summary>
        /// Listar eventos em ordem decrescente por valor unitário.
        /// </summary>
        /// <returns>Exibe a lista de eventos cadastrados.</returns>
        /// <response code="200">Listagem de eventos.</response>  
        /// <response code="404">Não há eventos cadastrados.</response>  
        [HttpGet]
        [Route("v1/eventos/valorunitario/desc")]
        public ObjectResult GetValorDesc() {
            var eventos = _eventoRepository.Listar();

            if(!eventos.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum evento encontrado.", eventos);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Eventos!", eventos.OrderByDescending(s => s.ValorUnitario));
        }        

        /// <summary>
        /// Buscar evento por ID.
        /// </summary>
        /// <returns>Exibe evento específico cadastrado por ID.</returns>
        /// <response code="200">Evento encontrado com sucesso.</response>  
        /// <response code="404">Evento não encontrado.</response>  
        [HttpGet]
        [Route("v1/eventos/{id}")]
        public ObjectResult Get(int id) {
            var evento = _eventoRepository.Buscar(id);
            
            if (evento == null) {
                return ResponseUtils.GenerateObjectResult("Evento não encontrado!", null);
            }
            
            var eventoListagem = new EventoListagemViewModel() {
                Id = evento.Id,
                Nome = evento.Nome,
                Capacidade = evento.Capacidade,
                Data = evento.Data,
                ValorUnitario = evento.ValorUnitario,
                CasaDeShowID = evento.CasaDeShowID,
                CasaDeShow = evento.CasaDeShow.Nome,
                GeneroMusicalID = evento.GeneroMusicalID,
                GeneroMusical = evento.GeneroMusical.Nome
            };
            
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Evento encontrado com sucesso!", eventoListagem);
        }

        /// <summary>
        /// Cadastrar evento.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Nome": "New Order - Show",
        ///        "Capacidade": 1000,
        ///        "Data": 04/03/2020,
        ///        "ValorUnitario": 750.00,
        ///        "CasaDeShowID": 1,
        ///        "GeneroMusicalID": 1,
        ///     }
        ///
        /// </remarks>
        /// <returns>Cadastra um novo evento.</returns>
        /// <response code="201">Evento criado com sucesso.</response>  
        /// <response code="400">Erro ao cadastrar evento.</response>  
        /// <response code="406">Model inválido.</response>  
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Evento>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status406NotAcceptable)]
        [Route("v1/eventos/")]
        public ObjectResult Post([FromBody] EventoCadastroViewModel eventoTemp) {
            if (eventoTemp == null) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Model inválido.", "Campo tipo inteiro");
            }
            if (!_casaDeShowRepository.Existe(eventoTemp.CasaDeShowID)) {
                ModelState.AddModelError("CasaDeShowId", "Casa de Show inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar evento.",
                    ModelState.ListarErros());
            }
            var evento = new Evento() {
                Id = 0,
                Nome = eventoTemp.Nome,
                Capacidade = eventoTemp.Capacidade,
                Data = eventoTemp.Data,
                ValorUnitario = eventoTemp.ValorUnitario,
                CasaDeShowID = eventoTemp.CasaDeShowID,
                GeneroMusicalID = eventoTemp.GeneroMusicalID
            };
            _eventoRepository.Criar(evento);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Evento cadastrado com sucesso!", 
                eventoTemp);
        }

        /// <summary>
        /// Editar evento.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Nome": "New Order - Show (editado)",
        ///        "Capacidade": 1000,
        ///        "Data": 04/03/2020,
        ///        "ValorUnitario": 750.00,
        ///        "CasaDeShowID": 1,
        ///        "GeneroMusicalID": 1,
        ///     }
        ///
        /// </remarks>
        /// <returns>Edita evento.</returns>
        /// <response code="201">Evento editado com sucesso.</response>  
        /// <response code="400">Erro ao editar evento.</response>  
        /// <response code="406">Model inválido.</response>  
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Evento>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>),StatusCodes.Status406NotAcceptable)]
        [Route("v1/eventos/{id}")]
        public ObjectResult Put(int id, [FromBody] EventoEdicaoViewModel eventoTemp) {
            if (eventoTemp == null) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Model inválido.", "Campo tipo inteiro");
            }
            if (id != eventoTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da casa de show.");
            }
            if (!_eventoRepository.Existe(eventoTemp.Id)) {
                ModelState.AddModelError("Id", "Evento inexistente.");
            }
            if (!_casaDeShowRepository.Existe(eventoTemp.CasaDeShowID)) {
                ModelState.AddModelError("CasaDeShowId", "Casa de Show inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar casa de show.",
                    ModelState.ListarErros());
            }
            var evento = new Evento() {
                Id = eventoTemp.Id,
                Nome = eventoTemp.Nome,
                Capacidade = eventoTemp.Capacidade,
                Data = eventoTemp.Data,
                ValorUnitario = eventoTemp.ValorUnitario,
                CasaDeShowID = eventoTemp.CasaDeShowID,
                GeneroMusicalID = eventoTemp.GeneroMusicalID
            };
            _eventoRepository.Editar(evento);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Evento editado com sucesso!", 
                eventoTemp);
        }
        
        /// <summary>
        /// Deleta um evento.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <returns>Deleta um evento.</returns>
        /// <response code="404">Evento não localizado.</response>  
        /// <response code="406">Relação não permitida para exclusão.</response>  
        [HttpDelete]
        [Route("v1/eventos/{id}")]
        public ObjectResult Delete(int id) {
            var evento = _eventoRepository.Buscar(id);
            if (evento == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Evento inexistente.", null);
            }
            try {
                _eventoRepository.Remover(evento);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Evento excluído com sucesso!", evento);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir o evento, contate o suporte!", evento);
            }
        }
    }
}    
