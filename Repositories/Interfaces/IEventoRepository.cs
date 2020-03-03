using System.Collections.Generic;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.EventoViewModels;

namespace Tickets_API.Repositories.Interfaces
{
    public interface IEventoRepository
    {
        Evento Buscar(int id);

        void Remover(Evento obj);

        void Editar(Evento obj);

        void Criar(Evento obj);
        bool Existe(int id);
        IEnumerable<EventoListagemViewModel> Listar();
    }
}