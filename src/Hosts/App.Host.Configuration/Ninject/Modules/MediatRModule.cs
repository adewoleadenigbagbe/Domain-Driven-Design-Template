using MediatR.Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Ninject.Extensions.Conventions;
using System.Reflection;

namespace App.Host.Configuration.Ninject.Modules
{
    public class MediatRModule : NinjectModule
    {
        public override void Load()
        {
            Kernel?.BindMediatR();

            var implementedTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
            };

            var infastructureAssembly = AppDomain.CurrentDomain.Load("App.Infastructure");
            foreach (var implementedType in implementedTypes)
            {
                Kernel?.Bind(x => x.From(infastructureAssembly)
                .SelectAllClasses()
                .InheritedFrom(implementedType)
                .BindAllInterfaces());
            }
        }
    }
}
