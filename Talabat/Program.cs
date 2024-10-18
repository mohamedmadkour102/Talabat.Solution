
<<<<<<< Updated upstream
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Stashed changes
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;

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

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"), 
                 B=>B.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName));
            });


            // Â” Œœ„ «Ê›—·Êœ  «‰Ì ·ÌÂ« ⁄‘«‰ «œÌ·Â «·ﬂÊ‰ﬂ‘‰ ” —Ì‰Ã 

            builder.Services.AddSingleton<IConnectionMultiplexer>(
                (serviceProvider) =>
                {
                    var connection = builder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(connection);
                });
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>(); instead of this 
<<<<<<< Updated upstream
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

=======
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(Repository.GenericRepository<>));
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            // «” Œœ «· «Ì» «Ê› ⁄·Ï ÿÊ· »œ· «·«œœ»—Ê›«Ì· œÌÂ 
            //builder.Services.AddAutoMapper(m => m.AddProfile(typeof(MappingProfiles)));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            { 
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };

                });
            // service of Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options=>
                {
                    // u can add configurations here 
                }).AddEntityFrameworkStores<AppIdentityDbContext>();

>>>>>>> Stashed changes

            var app = builder.Build();

            using var Scope = app.Services.CreateAsyncScope();
            var services = Scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var _userMnager = services.GetRequiredService<UserManager<AppUser>>();  

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _IdentityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeeding.SeedUserAsync(_userMnager);
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
               

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
