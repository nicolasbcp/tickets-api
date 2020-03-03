using System.Collections.Generic;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.GeneroMusicalViewModels;

namespace Tickets_API.Repositories.Interfaces
{
    public interface IGeneroMusicalRepository
    {
        GeneroMusical Buscar(int id);

        void Remover(GeneroMusical obj);

        void Editar(GeneroMusical obj);

        void Criar(GeneroMusical obj);
        bool Existe(int id);
        IEnumerable<GeneroMusicalListagemViewModel> Listar();
    }
}