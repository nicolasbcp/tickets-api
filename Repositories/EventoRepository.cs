using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Data;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.EventoViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EventoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(Evento obj)
        {
            _dbContext.Set<Evento>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Evento obj)
        {
            _dbContext.Set<Evento>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<Evento>().Any(x => x.Id == id);
        }

        public void Remover(Evento obj)
        {
            _dbContext.Set<Evento>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Evento Buscar(int id)
        {
            return _dbContext.Set<Evento>().Include(x => x.CasaDeShow).Include(x => x.GeneroMusical).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<EventoListagemViewModel> Listar()
        {
            return _dbContext.Set<Evento>().Select(x => new EventoListagemViewModel() { 
                Id = x.Id, 
                Nome = x.Nome,
                Capacidade = x.Capacidade,
                Data = x.Data,
                ValorUnitario = x.ValorUnitario,
                CasaDeShowID = x.CasaDeShowID,
                CasaDeShow = x.CasaDeShow.Nome,
                GeneroMusicalID = x.GeneroMusicalID,
                GeneroMusical = x.GeneroMusical.Nome
                }).ToList();
        }
    }
}