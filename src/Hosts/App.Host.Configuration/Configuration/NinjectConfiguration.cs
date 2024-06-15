using App.Host.Configuration.Ninject.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Host.Configuration.Configuration
{
    public class NinjectConfiguration
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Load(new DbConnectionModule(), new MediatRModule(), new AutoMapperModule());
        }
    }
}
