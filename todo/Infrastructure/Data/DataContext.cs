using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using todo.Domain.Entities;

namespace todo.Infrastructure.Data
{
    public class DataContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Domain.Entities.Task> Tasks { get; set; }
    }
}
