using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Application.Contract.Ports.Repositories;
using Chain.Domain;
using Chain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chain.Infrastructure.Persistence
{
    public class ChainDbContext : DbContext, IUnitOfWork
    {
        public ChainDbContext(DbContextOptions<ChainDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("PostgreSQL")
                , builder =>
            {
                builder.EnableRetryOnFailure();
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChainDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity<Guid>>())
                if (entry
                        .OriginalValues
                        .Properties
                        .FirstOrDefault(p => p.Name == nameof(Entity<Guid>.ConcurrencyStamp)) != entry
                        .CurrentValues
                        .Properties
                        .FirstOrDefault(p => p.Name == nameof(Entity<Guid>.ConcurrencyStamp)))
                    throw new InvalidOperationException("Your have data is changed. please refresh the page and get it again!");

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
