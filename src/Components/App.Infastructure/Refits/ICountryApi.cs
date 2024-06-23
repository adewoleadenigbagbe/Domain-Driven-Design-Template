using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App.Infastructure.Refits
{
    public interface ICountryApi
    {
        [Get("/countries/population/cities")]
        Task<CountryResponse> GetCountryPopulationByCities();
    }

    public class CountryResponse
    {
        public string Error { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        public List<Data> Data { get; set; }
    }

    public class Data
    {
        public string Country { get; set; }

        public string City { get; set; }

        public List<PopulationCounts> PopulationCounts { get; set; }
    }

    public class PopulationCounts 
    {
        public string Year { get; set; }

        public string Value { get; set; }

        public string Sex { get; set; }

        public string Reliabilty { get; set; }

    }
}
