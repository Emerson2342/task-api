using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Infra.Mappings;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMap());
        }
    }
}
