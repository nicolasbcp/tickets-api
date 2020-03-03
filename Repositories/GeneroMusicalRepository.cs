using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Data;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.GeneroMusicalViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Repositories
{
    public class GeneroMusicalRepository : IGeneroMusicalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GeneroMusicalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(GeneroMusical obj)
        {
            _dbContext.Set<GeneroMusical>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(GeneroMusical obj)
        {
            _dbContext.Set<GeneroMusical>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<GeneroMusical>().Any(x => x.Id == id);
        }

        public void Remover(GeneroMusical obj)
        {
            _dbContext.Set<GeneroMusical>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public GeneroMusical Buscar(int id)
        {
            return _dbContext.Set<GeneroMusical>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GeneroMusicalListagemViewModel> Listar()
        {
            return _dbContext.Set<GeneroMusical>().Select(x => new GeneroMusicalListagemViewModel() { 
                Id = x.Id, 
                Nome = x.Nome
                }).ToList();
        }
    }
}