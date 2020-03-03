using System.ComponentModel.DataAnnotations;

namespace Tickets_API.Models.ViewModels.UsuarioViewModels
{
    public class UsuarioCadastroViewModel
    {
        [Required]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(5, ErrorMessage="Mínimo de caracteres: 5.")]
        public string Nome {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(5, ErrorMessage="Mínimo de caracteres: 5.")]
        public string Email {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(5, ErrorMessage="Mínimo de caracteres: 5.")]
        public string Senha {get; set;}
    }
}