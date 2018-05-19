using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Util
{
    public class PtBrDateTimeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;
            var result = ModelBindingResult.Failed();
            DateTime outDate;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var values = value.Split('-');
                outDate = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                result = ModelBindingResult.Success(outDate);
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Data inválida");
            }

            bindingContext.Result = result;

            return Task.FromResult(0);
        }
    }
}
