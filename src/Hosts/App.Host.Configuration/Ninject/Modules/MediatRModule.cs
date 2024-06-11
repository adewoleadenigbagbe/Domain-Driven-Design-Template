using MediatR.Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Host.Configuration.Ninject.Modules
{
    public class MediatRModule : NinjectModule
    {
        public override void Load()
        {
            Kernel?.BindMediatR();
        }
    }
}
