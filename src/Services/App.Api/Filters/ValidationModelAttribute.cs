using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace App.Api.Filters
{
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new ValidationResultModel(actionContext.ModelState));
            }
            base.OnActionExecuting(actionContext);
        }
    }

    public class ValidationResultModel : IValidationResult
    {
        public ValidationResultModel()
        {
            ErrorMessage = "Validation Failed";
            Status = HttpStatusCode.BadRequest;
        }

        public ValidationResultModel(ModelStateDictionary modelState) : this()
        {
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.GroupBy(g => g.ErrorMessage).Select(x => new ValidationError(key, x.First().ErrorMessage)))
                .ToList();
        }

        public HttpStatusCode Status { get ; set; }

        public string ErrorMessage { get; set ; }

        public IEnumerable<IValidationError> Errors { get; set ; }
    }

    public interface IValidationResult
    {
        [JsonIgnore]
        HttpStatusCode Status { get; set; }

        [JsonIgnore]
        string ErrorMessage { get; set; }

        IEnumerable<IValidationError> Errors { get; set; }      
    }

    public class ValidationError : IValidationError
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }

        public ValidationError(string field, string message)
        {
            PropertyName = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public interface IValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string PropertyName { get; set; }

        string Message { get; set; }
    }
}
