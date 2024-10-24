using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TaskList.Components.Domain.Infra.Data;

namespace TaskList.Components.Domain.Extensions
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
            Configuration.Secrets.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
            Configuration.Secrets.PasswordSaltKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;

            Configuration.Email.UserName =
           builder.Configuration.GetSection("SmtpSettings").GetValue<string>("Username") ?? string.Empty;
            Configuration.Email.Password =
                builder.Configuration.GetSection("SmtpSettings").GetValue<string>("Password") ?? string.Empty;
            Configuration.Email.SmtpServer =
                builder.Configuration.GetSection("SmtpSettings").GetValue<string>("SmtpServer") ?? string.Empty;
            Configuration.Email.Port =
                builder.Configuration.GetSection("SmtpSettings").GetValue<int>("Port");

            Configuration.Ip.IpAddress = builder.Configuration.GetSection("IpConfig").GetValue<string>("IpAddress") ?? string.Empty;
            Configuration.Ip.IpAddress = builder.Configuration.GetSection("IpConfig").GetValue<string>("LocalHost") ?? string.Empty;
        }
        public static void AddDataBase(this WebApplicationBuilder builder)
        {
            string str = Configuration.Database.ConnectionString;
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 2));


            builder.Services.AddDbContext<AppDbContext>(
                x => x.UseMySql(str, serverVersion));
        }

        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {

                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            builder.Services.AddAuthorization();
        }

        public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lista de Tarefas", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                     new OpenApiSecurityScheme
                {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

            });
        }
    }
}
