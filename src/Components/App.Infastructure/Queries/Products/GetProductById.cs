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

namespace App.Infastructure.Queries.Products
{
    public static class GetProductById
    {
        public class Request : IRequest<Result>
        {
            public string Id { get; set;}
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


        public class Handler : IRequestHandler<Request, Result>
        {
            private readonly ReadAppContext _readAppContext;

            public Handler(ReadAppContext readAppContext)
            {
                _readAppContext = readAppContext;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _readAppContext.Products.FirstOrDefaultAsync();

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
