using System.Runtime.Serialization;

namespace Tickets_API.Models
{
    public class Usuario
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Email {get; set;}
        [IgnoreDataMember]
        public string Senha {get; set;}
        public string Role {get; set;}
    }
}