﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Core.Utils
{
    public static class ReadConfiguration
    {
        public static IConfiguration ReadAppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }
        public static IConfiguration ReadBasePathAppSettings()
        {
            IConfiguration configuration = null;
            var pathDocker = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../app"));
            var pathLocal = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Petalaka.Payment.API"));

            try
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(pathDocker)
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (configuration == null)
            {
                try
                {
                    configuration = new ConfigurationBuilder()
                        .SetBasePath(pathLocal)
                        .AddJsonFile("appsettings.Development.json")
                        .Build();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new FileNotFoundException("Unable to load configuration from any path.");
                }
            }
            return configuration;
        }
    }
}
