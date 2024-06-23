using App.Data.Contexts;
using App.Infastructure.Refits;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infastructure.Queries.Countrys
{
    public static class GetCityPopulation
    {
        public class Query : IRequest<CountryResponse>
        {
        }

        public class Handler : IRequestHandler<Query, CountryResponse>
        {
            private readonly ICountryApi _countryApi;

            public Handler(ICountryApi countryApi)
            {
                _countryApi = countryApi;
            }

            public async Task<CountryResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _countryApi.GetCountryPopulationByCities();
                return response;
            }
        }
    }
}
