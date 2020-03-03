using Microsoft.EntityFrameworkCore;
using Tickets_API.Models;

namespace Tickets_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
            
        public DbSet<CasaDeShow> CasasDeShow {get; set;}
        public DbSet<GeneroMusical> GenerosMusicais {get; set;}
        public DbSet<Evento> Eventos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Venda> Vendas {get; set;}
    }
}