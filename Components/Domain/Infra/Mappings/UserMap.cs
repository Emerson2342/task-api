using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            //pk
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address)
                     .HasColumnName("email_address")
                     .HasColumnType("VARCHAR(50)")
                     .IsRequired();
            });
            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.PassWord)
                        .HasColumnName("password")
                        .HasColumnType("VARCHAR(255)")
                        .IsRequired();
            });

            builder.Property(u => u.Name)
                   .HasColumnName("name")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired();

            builder.Property(u => u.IsEmailConfirmed)
                   .HasColumnName("is_email_confirmed")
                   .HasColumnType("TINYINT")
                   .IsRequired();
           
        }
    }

}
