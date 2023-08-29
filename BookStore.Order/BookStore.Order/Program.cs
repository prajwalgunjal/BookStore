using BookStore.Order.Context;
using BookStore.Order.Interface;
using BookStore.Order.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookStore.Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Orders V1", Version = "v1" });

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
            builder.Services.AddDbContext<Order_DB_Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookStore_Orders_Connection"));
            });
            builder.Services.AddTransient<IBookRepo, BookRepo>();
            builder.Services.AddTransient<IOrderRepo, OrderRepo>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();


            //jwt
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


            builder.Services.AddHttpClient("bookService", option =>
            {
                option.BaseAddress = new Uri(builder.Configuration["BaseURL:Book"]);
            });



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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