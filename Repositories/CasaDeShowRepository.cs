using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Data;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.CasaDeShowViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Repositories
{
    public class CasaDeShowRepository : ICasaDeShowRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CasaDeShowRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(CasaDeShow obj)
        {
            _dbContext.Set<CasaDeShow>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(CasaDeShow obj)
        {
            _dbContext.Set<CasaDeShow>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<CasaDeShow>().Any(x => x.Id == id);
        }

        public void Remover(CasaDeShow obj)
        {
            _dbContext.Set<CasaDeShow>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public CasaDeShow Buscar(int id)
        {
            return _dbContext.Set<CasaDeShow>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CasaDeShowListagemViewModel> Listar()
        {
            return _dbContext.Set<CasaDeShow>().Select(x => new CasaDeShowListagemViewModel() { Id = x.Id, Nome = x.Nome }).ToList();
        }
    }
}