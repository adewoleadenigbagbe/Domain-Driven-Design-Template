using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using App.Api;
using App.Host.Configuration.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Ninject;
using Owin;

namespace App.Host.SystemWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel();
            ConfigureWebApi(app,kernel);
            ConfigureStaticHosting(app);

            NinjectConfiguration.Configure(kernel);
        }

        private void ConfigureWebApi(IAppBuilder app, IKernel kernel)
        {
            var config = new HttpConfiguration();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            var setup = kernel.Get<Setup>();

            setup.ConfigureHttp(config);

            app.UseNinject(() => kernel).UseNinjectWebApi(config);          
        }

        private void ConfigureStaticHosting(IAppBuilder app)
        {
            var fileServerOptions = new FileServerOptions()
            {
                RequestPath = new PathString(""),
                EnableDirectoryBrowsing = true,
                FileSystem = new PhysicalFileSystem(@".\Views"),
                StaticFileOptions = { ServeUnknownFileTypes = true }
            };
            app.UseFileServer(fileServerOptions);
        }

        //configure hangfire
    }
}