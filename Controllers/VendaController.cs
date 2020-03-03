using System;
using System.Linq;
using GFT_Podcasts.Libraries.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets_API.Libraries.Utils.ExtensionsMethods;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.VendaViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Controllers
{
    [Route("api/")]
    public class VendaController : ControllerBase {
        private readonly IVendaRepository _vendaRepository;
        private readonly IEventoRepository _eventoRepository;
        
        public VendaController(IVendaRepository vendaRepository,IEventoRepository eventoRepository) {
                _vendaRepository = vendaRepository;
                _eventoRepository = eventoRepository;
        }

        [HttpGet]
        [Route("v1/vendas/{id}")]
        public ObjectResult Get(int id) {
            var venda = _vendaRepository.Buscar(id);
            
            if (venda == null) {
                return ResponseUtils.GenerateObjectResult("Venda não encontrada!", null);
            }
            
            var vendaListagem = new VendaListagemViewModel() {
                Id = venda.Id,
                EventoID = venda.EventoID,
                QuantidadeTicket = venda.QuantidadeTicket,
                TotalVenda = venda.TotalVenda,
                DataVenda = venda.DataVenda
            };
            
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Venda encontrada com sucesso!", vendaListagem);
        }

        [HttpGet]
        [Route("v1/vendas")]
        public ObjectResult Get() {
            var vendas = _vendaRepository.Listar();

            if(!vendas.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhuma venda encontrada.", vendas);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de vendas!", vendas);
        }
        
        [HttpPost]
        [Route("v1/vendas/")]
        public ObjectResult Post([FromBody] VendaCadastroViewModel vendaTemp) {
            if (!_vendaRepository.Existe(vendaTemp.EventoID)) {
                ModelState.AddModelError("EventoId", "Evento inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar venda.",
                    ModelState.ListarErros());
            }
            var venda = new Venda() {
                Id = 0,
                EventoID = vendaTemp.EventoID,
                QuantidadeTicket = vendaTemp.QuantidadeTicket,
                DataVenda = vendaTemp.DataVenda
            };
            _vendaRepository.Criar(venda);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Venda cadastrada com sucesso!", 
                vendaTemp);
        }
        
        [HttpPut]
        [Route("v1/vendas/{id}")]
        public ObjectResult Put(int id, [FromBody] VendaEdicaoViewModel vendaTemp) {
            if (id != vendaTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da venda.");
            }
            if (!_vendaRepository.Existe(vendaTemp.Id)) {
                ModelState.AddModelError("Id", "Venda inexistente.");
            }
            if (!_eventoRepository.Existe(vendaTemp.EventoID)) {
                ModelState.AddModelError("EventoId", "Evento inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar venda.",
                    ModelState.ListarErros());
            }
            var venda = new Venda() {
                Id = 0,
                EventoID = vendaTemp.EventoID,
                QuantidadeTicket = vendaTemp.QuantidadeTicket,
                DataVenda = vendaTemp.DataVenda
            };
            _vendaRepository.Editar(venda);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Venda editada com sucesso!", 
                vendaTemp);
        }
        
        [HttpDelete]
        [Route("v1/vendas/{id}")]
        public ObjectResult Delete(int id) {
            var venda = _vendaRepository.Buscar(id);
            if (venda == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Venda inexistente.", null);
            }
            try {
                _vendaRepository.Remover(venda);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Venda excluída com sucesso!", venda);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir a venda, contate o suporte!", venda);
            }
        }
    }
}