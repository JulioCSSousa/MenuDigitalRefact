
using AutoMapper;
using CloudinaryDotNet;
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Application.Services;
using MenuDigital.Domain.Interfaces;
using MenuDigital.Domain.Models.Entities;
using MenuDigital.Infrastructure.Context.Repositories;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using MenuDigital.Infrastructure.Repositories;
using MenuDigital.Infrastructure.Repositories.MenuRepository;
using MenuDigital.Infrastructure.Repositories.StoreRepository;
using MenuDigital.Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.Services;

namespace MenuDigitalApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 10));


            string cs;

            if (builder.Environment.IsProduction())
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
                var dbUser = Environment.GetEnvironmentVariable("DB_USER");
                var dbPass = Environment.GetEnvironmentVariable("DB_PASS");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");

                cs = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPass};";
            }
            else
            {
                cs = $"Server=localhost;Database=menudigitaldb;User=root;Password=4306";
            }

            builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, serverVersion,
        b => b.MigrationsAssembly("MenuDigital.Infrastructure")));

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "MenuDigital API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            var cloudName = builder.Configuration["Cloudinary:CloudName"];
            var apiKey = builder.Configuration["Cloudinary:ApiKey"];
            var apiSecret = builder.Configuration["Cloudinary:ApiSecret"];

            var account = new Account(cloudName, apiKey, apiSecret);
            var cloudinary = new Cloudinary(account);

            builder.Services.AddSingleton(cloudinary);

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IStorePaymentRepository, StorePaymentRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            // Services de aplicação
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<StoreService>();
            builder.Services.AddScoped<IWorkScheduleService, WorkScheduleService>();
            builder.Services.AddScoped<StorePaymentService>();
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<TokenService>();


            var jwtKey = builder.Configuration["Jwt:Key"];
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization();

            if (builder.Environment.IsProduction())
            {
                var port = Environment.GetEnvironmentVariable("PORT");
                builder.WebHost.UseUrls($"http://*:{port}");
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MenuDigital API V1");
                c.RoutePrefix = "swagger"; 
            });

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCors("AllowAll");
            app.MapGet("/", () => Results.Ok("✅ MenuDigital API Running"));
            app.MapControllers();

            app.Run();
        }
    }
}
