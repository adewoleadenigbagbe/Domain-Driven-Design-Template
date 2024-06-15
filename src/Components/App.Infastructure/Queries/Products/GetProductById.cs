using App.Data.Contexts;
using App.Data.Entities;
using App.Data.Helpers;
using App.Infastructure.BasicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using App.Infastructure.Models;

using Newtonsoft.Json;

namespace App.Infastructure.Queries.Products
{
    public static class GetProductById
    {
        public class Query : IRequest<Result>
        {
            [JsonIgnore]
            public Guid Id { get; set;}
        }

        public class Result : BasicResult
        {
            public ProductModel Product { get; set;}

            public Result(HttpStatusCode code, string message) : base(code,message)
            {
            }

            public Result(ProductModel product) : base()
            {
                Product = product;
            }
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
                var product = await _readAppContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null)
                {
                    return new Result(HttpStatusCode.NotFound,"Product not found");
                }

                //use AutoMapper Lib
                var productModel = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Vat = product.Vat,
                };


                return new Result(productModel);
            }
        }

    }
}
