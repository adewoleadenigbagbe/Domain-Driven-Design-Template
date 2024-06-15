using App.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using MediatR;

using App.Data.Entities;
using App.Data.Helpers;
using App.Infastructure.BasicResults;

namespace App.Infastructure.Commands
{
    public static class CreateProduct
    {
        public class Request : IRequest<Result>
        {
            [MaxLength(100)]
            [Required]
            public string Name { get; set; }

            [Required]
            public decimal Price { get; set; }

            [Required]
            public decimal Vat { get; set; }

            public Category Category { get; set; }
        }

        public class Result : BasicResult
        {
            public Guid Id { get; set; }

            public Result(string message) : base(message)
            {
            }

            public Result(Guid id) : base()
            {
                Id = id;
            }
        }


        public class Handler : IRequestHandler<Request, Result>
        {
            private readonly ReadWriteAppContext _readWriteAppContext;

            public Handler(ReadWriteAppContext readWriteAppContext)
            {
                _readWriteAppContext = readWriteAppContext;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var id = SequentialGuid.Create();
                var now = DateTime.Now;

                var product = new Product
                {
                    Id = id,
                    Name = request.Name,
                    Vat = request.Vat,
                    Category = request.Category,
                    Price = request.Price,
                    IsDeprecated = false,
                    CreatedOn = now,
                    ModifiedOn = now
                };

                _readWriteAppContext.Products.Add(product);

                await _readWriteAppContext.SaveChangesAsync();

                return new Result(id);
            }
        }
    }
}
