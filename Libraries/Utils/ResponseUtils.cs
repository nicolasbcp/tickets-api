using Tickets_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Tickets_API.Libraries.Utils {
    public static class ResponseUtils {
        public static ObjectResult GenerateObjectResult(string mensagem, object dado = null){
            return new ObjectResult(new ResultViewModel<object>(mensagem, dado));
        }
    }
}