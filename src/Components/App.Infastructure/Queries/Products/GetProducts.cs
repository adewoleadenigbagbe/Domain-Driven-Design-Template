using App.Data.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using App.Infastructure.Models;


namespace App.Infastructure.Queries.Products
{
    public static class GetProducts
    {
        public class Request : IRequest<Result>
        {

        }

        public class Result
        {
            public HttpStatusCode StatusCode { get; set; }

            public List<ProductModel> Products { get; set; }
        }


        public class Handler : IRequestHandler<Request, Result>
        {
            private readonly ReadAppContext _readAppContext;

            public Handler(ReadAppContext readAppContext)
            {
                _readAppContext = readAppContext;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {

                var products = await _readAppContext.Products.Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Vat = p.Vat
                }).ToListAsync();

                var result = new Result
                {
                    StatusCode = HttpStatusCode.OK,
                    Products = products
                };

                return result;
            }
        }

    }
}
