
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Errors;
using Talabat.Helpers;
using Talabat.MiddleWares;
using Talabat.Repository;
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
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           /// app.UseStatusCodePagesWithRedirects("/Errors/{0}");
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");



            // app.UseDeveloperExceptionPage(); 
            // mn awel .net 6.0 , msh lazem t3mlha register , 2bl kda lazem 
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
