using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Host.Configuration.Configuration
{
    public class AutoMapperConfiguration
    {
        public static IConfigurationProvider Configure(IEnumerable<Profile> profiles)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfiles(profiles);
            });

            return config;
        }
    }
}
