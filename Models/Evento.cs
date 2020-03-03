using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tickets_API.Models
{
    public class Evento
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public int Capacidade {get; set;}
        public DateTime Data {get; set;}
        public float ValorUnitario {get; set;}

        [ForeignKey("CasaDeShowID")]
        public CasaDeShow CasaDeShow {get; set;}
        public int CasaDeShowID {get; set;}
        
        [ForeignKey("GeneroMusicalID")]
        public GeneroMusical GeneroMusical {get; set;}
        public int GeneroMusicalID {get; set;}
    }
}