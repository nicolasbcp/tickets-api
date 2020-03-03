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
    }
}