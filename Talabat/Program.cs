
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>(); instead of this 
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            var app = builder.Build();

            using var Scope = app.Services.CreateAsyncScope();
            var services = Scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                StoreContextSeed.SeedAsync(_dbContext);


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occure During Migration");
            }

            // ﬂœÂ ﬁœ—‰« ‰Â‰œ· «‰Â ·Ê Õ’· Õ«ÃÂ Ê«‰  » ⁄„· «·„ÌÃÌ—‘‰ «‰Â „Ìﬁ›‘ 
            // Â«ÌÃÌ» «··Ì ⁄‰œÌ ›Ì «·ﬂ« ‘ ÊÌ⁄„· —«‰ ⁄«œÌ 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
