using System.Collections.Generic;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.VendaViewModels;

namespace Tickets_API.Repositories.Interfaces
{
    public interface IVendaRepository
    {
        Venda Buscar(int id);

        void Remover(Venda obj);

        void Editar(Venda obj);

        void Criar(Venda obj);
        bool Existe(int id);
        IEnumerable<VendaListagemViewModel> Listar();
    }
}