using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using App.Host.SystemWeb;

using Microsoft.Owin.Hosting;

namespace App.Host.SelfHost
{
    class Program
    {
        static void Main()
        {
            const string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Ready on {baseAddress}.");
                Console.ReadLine();
            }
        }
    }
}
