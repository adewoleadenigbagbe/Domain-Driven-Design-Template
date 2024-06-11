using App.Data.Contexts;
using App.Data.MySQL;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Host.Configuration.Ninject.Modules
{
    public class DbConnectionModule : NinjectModule
    {
        public override void Load()
        {
            Kernel?.Bind<ReadAppContext>().To<MySqlReadAppContext>().InTransientScope();
            Kernel?.Bind<ReadWriteAppContext>().To<MySqlReadWriteAppContext>().InTransientScope();
        }
    }
}
