using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tickets_API.Models
{
    public class Venda
    {
        public int Id {get; set;}
        [ForeignKey("EventoID")]
        public Evento Evento {get; set;}
        public int EventoID {get; set;}
        public int QuantidadeTicket {get; set;}
        public float TotalVenda {get; set;}
        public DateTime DataVenda {get; set;}
    }
}