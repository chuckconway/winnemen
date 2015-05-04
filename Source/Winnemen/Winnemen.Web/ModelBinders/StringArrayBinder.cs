using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Winnemen.Web.ModelBinders
{
    public class StringArrayBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = Convert.ToString(val.RawValue);
            var strings = value.Split(',');

            bindingContext.Model = strings.Select(s => s.Trim()).ToArray();
            return true;
        }
    }
}
