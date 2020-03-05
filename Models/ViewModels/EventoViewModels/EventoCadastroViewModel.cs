using System;
using System.ComponentModel.DataAnnotations;

namespace Tickets_API.Models.ViewModels.EventoViewModels
{
    public class EventoCadastroViewModel
    {
        [Required]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(5, ErrorMessage="Mínimo de caracteres: 5.")]
        public string Nome {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [Range(1, 100000)]
        public int Capacidade {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public DateTime Data {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [Range(1, 100000)]
        public float ValorUnitario {get; set;}
        [Required(ErrorMessage="Não possível cadastrar evento, pois não existem casas de show.")]
        [Range(1, 100000)]
        public int CasaDeShowID {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [Range(1, 100000)]
        public int GeneroMusicalID {get; set;}
    }
}