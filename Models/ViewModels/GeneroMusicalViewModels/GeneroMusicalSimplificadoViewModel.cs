using System.ComponentModel.DataAnnotations;

namespace Tickets_API.Models.ViewModels.GeneroMusicalViewModels
{
    public class GeneroMusicalSimplificadoViewModel
    {
        [Required]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(3, ErrorMessage="Mínimo de caracteres: 3.")]
        public string Nome {get; set;}
    }
}