using BookStore.Books.Context;
using BookStore.Books.Interface;
using BookStore.Books.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookStore.Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Product V1", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Using Authorization header with the beare schema",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                option.AddSecurityDefinition("Bearer", securitySchema);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema , new[] { "Bearer" } }
                });

            });

            // Add services to the container.
            builder.Services.AddTransient<IBookRepo, BookRepo>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // swagger code -> Authentication

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
            builder.Services.AddDbContext<BooksDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookStore_Books_Connection"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}