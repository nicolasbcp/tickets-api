using System;
using System.Linq;
using GFT_Podcasts.Libraries.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
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
                GeneroMusicalID = evento.GeneroMusicalID
            };
            
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Evento encontrado com sucesso!", eventoListagem);
        }

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
        
        [HttpPost]
        [Route("v1/eventos/")]
        public ObjectResult Post([FromBody] EventoCadastroViewModel eventoTemp) {
            if (!_casaDeShowRepository.Existe(eventoTemp.CasaDeShowID)) {
                ModelState.AddModelError("CasaDeShowId", "Casa de Show inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar casa de show.",
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
            return ResponseUtils.GenerateObjectResult("Casa de Show cadastrada com sucesso!", 
                eventoTemp);
        }
        
        [HttpPut]
        [Route("v1/eventos/{id}")]
        public ObjectResult Put(int id, [FromBody] EventoEdicaoViewModel eventoTemp) {
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
