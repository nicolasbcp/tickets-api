using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tickets_API.Models.ViewModels.UsuarioViewModels
{
    public class UsuarioSimplificadoViewModel
    {
        [Required]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [MinLength(5, ErrorMessage="Favor entrar com endereço de e-mail válido.")]
        public string Email {get; set;}
        [IgnoreDataMember]
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public string Senha {get; set;}
    }
}