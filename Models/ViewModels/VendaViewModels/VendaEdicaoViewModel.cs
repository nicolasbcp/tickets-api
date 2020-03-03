using System;
using System.ComponentModel.DataAnnotations;

namespace Tickets_API.Models.ViewModels.VendaViewModels
{
    public class VendaEdicaoViewModel
    {
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public int Id {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]    
        public int EventoID {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public int QuantidadeTicket {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public float TotalVenda {get; set;}
        [Required(ErrorMessage="Este campo é obrigatório.")]
        public DateTime DataVenda {get; set;}
    }
}