using System;
using System.ComponentModel.DataAnnotations;

namespace Tickets_API.Models.ViewModels.EventoViewModels
{
    public class EventoListagemViewModel
    {
        [Required]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage="Limite de caracteres: 100.")]
        [MinLength(5, ErrorMessage="Mínimo de caracteres: 5.")]
        public string Nome {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public int Capacidade {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public DateTime Data {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public float ValorUnitario {get; set;}
        [Required(ErrorMessage="Não possível cadastrar evento, pois não existem casas de show.")]
        public int CasaDeShowID {get; set;}
        public string CasaDeShow {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public int GeneroMusicalID {get; set;}
        public string GeneroMusical {get; set;}

    }
}