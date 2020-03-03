using System;

namespace Tickets_API.Models.ViewModels
{
    /**
     * Classe que será retornada nas requisições HTTP da API.
     */
    public class ResultViewModel
    {
        public ResultViewModel(string mensagem, object dado) {
            Mensagem = mensagem;
            Dado = dado;
        }

        public string Mensagem { get; set; }
        public Object Dado { get; set; }
    }
}