using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using MediatR;

using App.Infastructure.Commands;
using App.Infastructure.Queries.Products;


namespace App.Api.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductController : ApiController
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateProductAsync(CreateProduct.Request request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProductsAsync(GetProducts.Query request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductById(GetProductById.Query request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
