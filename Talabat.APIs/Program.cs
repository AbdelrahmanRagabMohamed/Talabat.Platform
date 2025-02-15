using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.Repository;
using Talabat.Repository.Data;

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

            // Allow Dependency Injection for my Context (StoreContext)
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }

            );

            builder.Services.AddApplicationServices(); // Extension Method

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

                // call Seeding functions 
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

            app.UseStaticFiles(); // We must add this Middleware To can Read static files (such as images)

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
