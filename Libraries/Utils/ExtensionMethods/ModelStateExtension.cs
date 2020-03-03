using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tickets_API.Libraries.Utils.ExtensionsMethods {
    public static class ModelStateExtension {
        public static List<string> ListarErros(this ModelStateDictionary modelstate) {
            return modelstate
                .Values.SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage + " " + v.Exception).ToList();
        }
    }
}