using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using MediatR;

using App.Infastructure.Models;
using App.Common.Interfaces;
using App.Common.Helpers;
using App.Data.Contexts;
using App.Data.Entities.Customers;


namespace App.Infastructure.Queries.Products
{
    public static class GetProducts
    {
        public class Query : IRequest<Result>, IPagedRequest
        {
            public int Page { get; set; } = 1;

            public int PageLength { get; set; } = 100;
        }

        public class Result : PagedResult<ProductModel>
        {
            public HttpStatusCode StatusCode { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ReadAppContext _readAppContext;

            public Handler(ReadAppContext readAppContext)
            {
                _readAppContext = readAppContext;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = new Result
                {
                    StatusCode = HttpStatusCode.OK
                };

                result = await _readAppContext.Products.Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Vat = p.Vat
                }).OrderBy(x => x.Id).ToPageResultAsync<ProductModel, Result>(request);


                return result;
            }
        }

    }
}
