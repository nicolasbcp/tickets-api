using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tickets_API.Data;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.UsuarioViewModels;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(Usuario obj)
        {
            _dbContext.Set<Usuario>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Usuario obj)
        {
            _dbContext.Set<Usuario>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<Usuario>().Any(x => x.Id == id);
        }

        public void Remover(Usuario obj)
        {
            _dbContext.Set<Usuario>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Usuario Buscar(int id)
        {
            return _dbContext.Set<Usuario>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UsuarioListagemViewModel> Listar()
        {
            return _dbContext.Set<Usuario>().Select(x => new UsuarioListagemViewModel() { 
                Id = x.Id, 
                Nome = x.Nome,
                Email = x.Email
                }).ToList();
        }
    }
}