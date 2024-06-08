using App.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using App.Data.Entities;
using System.ComponentModel.DataAnnotations;
using App.Data.Helpers;
using App.Infastructure.BasicResults;

namespace App.Infastructure.Commands
{
    public static class CreateProduct
    {
        public class Command : IRequest<Result>
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


        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly ReadWriteAppContext _readWriteAppContext;

            public Handler(ReadWriteAppContext readWriteAppContext)
            {
                _readWriteAppContext = readWriteAppContext;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var id = SequentialGuid.Create();

                var product = new Product
                {
                    Id = id,
                    Name = request.Name,
                    Vat = request.Vat,
                    Category = request.Category
                };

                _readWriteAppContext.Products.Add(product);

                return new Result(id);
            }
        }
    }
}
