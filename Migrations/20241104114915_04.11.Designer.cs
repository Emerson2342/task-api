﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskList.Components.Domain.Infra.Data;

#nullable disable

namespace TaskList.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241104114915_04.11")]
    partial class _0411
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TaskList.Components.Domain.Main.Entities.TaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<DateOnly>("Deadline")
                        .HasColumnType("date")
                        .HasAnnotation("Relational:JsonPropertyName", "deadLine");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<DateOnly>("StartTime")
                        .HasColumnType("date")
                        .HasAnnotation("Relational:JsonPropertyName", "startTime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasAnnotation("Relational:JsonPropertyName", "userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tasklist", (string)null);
                });

            modelBuilder.Entity("TaskList.Components.Domain.Main.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<sbyte>("IsEmailConfirmed")
                        .HasColumnType("TINYINT")
                        .HasColumnName("is_email_confirmed")
                        .HasAnnotation("Relational:JsonPropertyName", "isEmailConfirmed");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("Relational:JsonPropertyName", "token");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasAnnotation("Relational:JsonPropertyName", "user");
                });

            modelBuilder.Entity("TaskList.Components.Domain.Main.Entities.TaskEntity", b =>
                {
                    b.HasOne("TaskList.Components.Domain.Main.Entities.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskList.Components.Domain.Main.Entities.User", b =>
                {
                    b.OwnsOne("TaskList.Components.Domain.Main.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("VARCHAR(50)")
                                .HasColumnName("email_address");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.HasAnnotation("Relational:JsonPropertyName", "email");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("TaskList.Components.Domain.Main.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("PassWord")
                                .IsRequired()
                                .HasColumnType("VARCHAR(255)")
                                .HasColumnName("password");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.HasAnnotation("Relational:JsonPropertyName", "password");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("TaskList.Components.Domain.Main.Entities.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}