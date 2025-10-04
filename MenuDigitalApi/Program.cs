
using AutoMapper;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


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
                cs = builder.Configuration.GetConnectionString("DefaultConnection");
            }

            builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, serverVersion,
        b => b.MigrationsAssembly("MenuDigital.Infrastructure")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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

            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

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

            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.MapGet("/", () => Results.Ok("✅ MenuDigital API Running"));
            app.MapControllers();

            app.Run();
        }
    }
}
