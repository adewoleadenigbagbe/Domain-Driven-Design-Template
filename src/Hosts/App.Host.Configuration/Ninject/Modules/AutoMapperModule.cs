using App.Host.Configuration.Configuration;
using App.Infastructure.AutoMapper;
using AutoMapper;
using Ninject.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Ninject.Extensions.Conventions;
using Ninject;


namespace App.Host.Configuration.Ninject.Modules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var profilesAssembly = Assembly.GetAssembly(typeof(ProductProfile));

            Kernel?.Bind(x => x.From(profilesAssembly)
             .SelectAllTypes()
             .Where(a => typeof(Profile).IsAssignableFrom(a))
             .BindBase());

            var profiles = Kernel?.GetAll<Profile>() ?? Enumerable.Empty<Profile>();

            var mapperConfiguration = AutoMapperConfiguration.Configure(profiles);

            Kernel?.Bind<IConfigurationProvider>().ToConstant(mapperConfiguration);
            Kernel?.Bind<IMapper>().ToMethod(x => x.Kernel.Get<IConfigurationProvider>().CreateMapper()).InSingletonScope();
        }
    }
}
