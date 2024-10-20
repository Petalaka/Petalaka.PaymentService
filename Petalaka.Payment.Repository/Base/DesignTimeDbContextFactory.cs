using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Petalaka.Payment.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Base
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PetalakaDbContext>
    {
        public DesignTimeDbContextFactory()
        {
        }
        public PetalakaDbContext CreateDbContext(string[] args)
        {
            var configuration = ReadConfiguration.ReadBasePathAppSettings();
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
            }
            //var configuration = CoreHelper.GetDbDesignTimeAppSettings;
            var builder = new DbContextOptionsBuilder<PetalakaDbContext>();
            builder.UseSqlServer(connectionString);
            return new PetalakaDbContext(builder.Options);
        }
    }
}
