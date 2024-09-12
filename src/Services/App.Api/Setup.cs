using App.Api.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace App.Api
{
    public class Setup
    {
        public void ConfigureHttp(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Services.Add(typeof(IExceptionLogger), typeof(NLogExceptionLogger));
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new TrimConverter());
        }
    }
}
