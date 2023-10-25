using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace Chain.Infrastructure.Persistence
{
    public class ChainDbContextFactory : IDbContextFactory<ChainDbContext>
    {
        public ChainDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChainDbContext>();
            optionsBuilder.UseNpgsql(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("PostgreSQL")
                , builder =>
                {
                    builder.EnableRetryOnFailure();
                });

            return new ChainDbContext(optionsBuilder.Options);
        }
    }
}
