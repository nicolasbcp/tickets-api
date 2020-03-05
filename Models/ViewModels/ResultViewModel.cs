namespace Tickets_API.Models.ViewModels
{
    /**
     * Classe que será retornada nas requisições HTTP da API.
     */
    public class ResultViewModel<T>
    {
        public ResultViewModel(string mensagem, T dado) {
            Mensagem = mensagem;
            Dado = dado;
        }

        public string Mensagem { get; set; }
        public T Dado { get; set; }
    }
}