using App.Infastructure.Queries.Countrys;
using App.Infastructure.Queries.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace App.Api.Controllers
{
    [RoutePrefix("api/v1/country")]
    public class CountryController : ApiController
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("population/cities")]
        public async Task<IHttpActionResult> GetCityPopulation([FromBody] GetCityPopulation.Query request)
        {
            request = request ?? new GetCityPopulation.Query();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
