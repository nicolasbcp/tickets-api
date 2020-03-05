using System.Runtime.Serialization;

namespace Tickets_API.Models
{
    public class GeneroMusical
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        [IgnoreDataMember]
        public string Imagem {get; set;}
    }
}