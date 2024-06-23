using App.Infastructure.Refits;
using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Net.Http;
using System.Collections.Specialized;

using Refit;
using System.Diagnostics;

namespace App.Host.Configuration.Ninject.Modules
{
    public class RefitModule : NinjectModule
    {
        private readonly NameValueCollection _connectionStrings = ConfigurationManager.AppSettings;
        public override void Load()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_connectionStrings["Country.Url"])
            };

            var countryAPi = RestService.For<ICountryApi>(httpClient);

            Kernel?.Bind<ICountryApi>().ToMethod(context => countryAPi).InSingletonScope();
        }
    }
}
