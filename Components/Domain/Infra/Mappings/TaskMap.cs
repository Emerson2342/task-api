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
           
            builder.HasOne(t =>t.User).WithMany(u=>u.Tasks).IsRequired();

        }
    }
}
