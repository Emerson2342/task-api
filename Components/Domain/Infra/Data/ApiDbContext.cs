using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Infra.Mappings;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Infra.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
