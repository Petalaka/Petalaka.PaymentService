using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petalaka.Payment.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Base
{
    public class DbInitializer
    {
        private readonly PetalakaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DbInitializer(PetalakaDbContext dbContext,
            ILogger<DbInitializer> logger,
            IUnitOfWork unitOfWork
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _dbContext.Database.MigrateAsync();
                //await SeedDataAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in initialize database ", ex);
                throw;
            }
        }
    }
}
