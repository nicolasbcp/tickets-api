using System.Collections.Generic;
using Tickets_API.Models;
using Tickets_API.Models.ViewModels.UsuarioViewModels;

namespace Tickets_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Buscar(int id);

        void Remover(Usuario obj);

        void Editar(Usuario obj);

        void Criar(Usuario obj);
        bool Existe(int id);
        IEnumerable<UsuarioListagemViewModel> Listar();
    }
}