using System.Collections.Generic;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.CasaDeShowViewModels;

namespace Tickets_API.Repositories.Interfaces
{
    public interface ICasaDeShowRepository
    {
        CasaDeShow Buscar(int id);
        CasaDeShow BuscarNome(string nome);

        void Remover(CasaDeShow obj);

        void Editar(CasaDeShow obj);

        void Criar(CasaDeShow obj);
        bool Existe(int id);
        IEnumerable<CasaDeShowListagemViewModel> Listar();
    }
}