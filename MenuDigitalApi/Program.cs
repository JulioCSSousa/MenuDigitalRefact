
using AutoMapper;
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Application.Mapper;
using MenuDigital.Application.Services;
using MenuDigital.Domain.Interfaces;
using MenuDigital.Domain.Models.Entities;
using MenuDigital.Infrastructure.Context.Repositories;
using MenuDigital.Infrastructure.Mongo;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using MenuDigital.Infrastructure.Repositories;
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

            // pegar string de conexão do appsettings.json
            var mongoConn = builder.Configuration.GetConnectionString("MongoDb")!;
            var mongoDbName = builder.Configuration["MongoDbName"] ?? "MenuDigitalDb";

            // registra MongoDbService como singleton
            builder.Services.AddSingleton(new MongoDbService(mongoConn, mongoDbName));

            // MySQL Conection
            var cs = builder.Configuration.GetConnectionString("LocalSQL");
            builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(cs, serverVersion,
        b => b.MigrationsAssembly("MenuDigital.Infrastructure")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IStorePaymentRepository, StorePaymentRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
            // Services de aplicação
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<StoreService>();
            builder.Services.AddScoped<IWorkScheduleService, WorkScheduleService>();
            builder.Services.AddScoped<StorePaymentService>();

            builder.Services.AddAutoMapper(cfg => {

            }, typeof(ProductProfile) );


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

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var mongo = scope.ServiceProvider.GetRequiredService<MongoDbService>();
            }

            app.MapGet("/", () => "API Mongo conectada!");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
