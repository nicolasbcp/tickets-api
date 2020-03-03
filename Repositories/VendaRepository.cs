using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Data;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.VendaViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VendaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(Venda obj)
        {
            _dbContext.Set<Venda>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Venda obj)
        {
            _dbContext.Set<Venda>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<Venda>().Any(x => x.Id == id);
        }

        public void Remover(Venda obj)
        {
            _dbContext.Set<Venda>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Venda Buscar(int id)
        {
            return _dbContext.Set<Venda>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<VendaListagemViewModel> Listar()
        {
            return _dbContext.Set<Venda>().Include(x => x.Evento).Select(x => new VendaListagemViewModel() { 
                Id = x.Id, 
                EventoID = x.EventoID,
                QuantidadeTicket = x.QuantidadeTicket,
                TotalVenda = x.TotalVenda,
                DataVenda = x.DataVenda
                }).ToList();
        }
    }
}