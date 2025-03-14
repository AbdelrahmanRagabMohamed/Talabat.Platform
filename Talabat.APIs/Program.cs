using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entites.Identity;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services - Add services to the container.

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Allow Dependency Injection for Buisness Context (StoreContext)
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            // Allow Dependency Injection for AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // Allow Dependency Injection For Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");

                return ConnectionMultiplexer.Connect(Connection);

            });

            builder.Services.AddApplicationServices(); // Extension Method

            builder.Services.AddIdentityServices(builder.Configuration);    // Extension Method 

            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", Policy =>
                {
                    Policy.AllowAnyHeader();
                    Policy.AllowAnyMethod();
                    Policy.WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });


            #endregion


            var app = builder.Build();


            #region Update - Database

            // Group os Services that its lifetime is Scoped
            using var Scope = app.Services.CreateScope();

            // Get the Services Its self
            var Services = Scope.ServiceProvider;

            var LoogerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                // Ask ClR to Create object from StoreContext Explicitly
                var dbContext = Services.GetRequiredService<StoreContext>();

                // Update - Database
                await dbContext.Database.MigrateAsync();

                var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();  // Ask ClR to Create object from StoreContext Explicitly
                await IdentityDbContext.Database.MigrateAsync();   // Update - Database


                // Ask ClR to Create object from UserManager<AppUser> Explicitly
                var UserManger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(UserManger);

                // Call Seeding functions 
                await StoreContextSeed.SeedAsync(dbContext);

            }

            catch (Exception ex)
            {
                var looger = LoogerFactory.CreateLogger<Program>();
                looger.LogError(ex, "An Error Occured During Applaying Migration");

            }
            #endregion


            #region Configure - Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();   // Custom Middleware for Internal Server Error

                app.UseSwaggerMiddlewares(); // Extension Method
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles(); // We Must Add This Middleware To Can Read Static Files (Such as Images)

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
