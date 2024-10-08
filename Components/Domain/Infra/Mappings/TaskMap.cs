using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.ValueObjects;

namespace TaskList.Components.Domain.Infra.Mappings
{
    public class TaskMap : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("tasklist");
            builder.HasKey(t => t.UserId);

            builder.OwnsMany(t => t.Tasks)
                .Property(x =>x.Title)
                .HasColumnName("title")
                .HasColumnType("VARCHAR(30)")
                .IsRequired();

            builder.OwnsMany(t => t.Tasks)
                .Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.OwnsMany(t => t.Tasks)
               .Property(x => x.StartTime)
               .HasColumnName("started_at")
               .HasColumnType("DATETIME")
               .IsRequired();
            builder.OwnsMany(t => t.Tasks)
               .Property(x => x.Deadline)
               .HasColumnName("deadline")
               .HasColumnType("DATETIME")
               .IsRequired();




        }
    }
}
