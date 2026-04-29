
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Nhs.Test.Api.BusinessService;
using Nhs.Test.Api.Filters;
using Nhs.Test.Api.Mapping;
using Nhs.Test.Api.Model;
using Nhs.Test.Api.Mapping;
namespace Nhs.Test.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase(builder.Configuration.GetConnectionString("AppDb")));
            builder.Services.AddControllers(o => o.Filters.Add(new GlobalExceptionHandler()));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AMappingProfile>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            // app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var contex = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedData(contex);
            }
            app.Run();
        }

        private static void SeedData(AppDbContext context)
        {
            context.Database.EnsureCreated();
            context.AddRange(
                new Patient { Id = 1, DateOfBirth = DateTime.Now, GPPractice = "GP1", Name = "John", NHSNumber = "a1" },
                new Patient { Id = 8, DateOfBirth = DateTime.Now, GPPractice = "GP2", Name = "peter", NHSNumber = "a2" },
                new Patient { Id = 2, DateOfBirth = DateTime.Now, GPPractice = "GP3", Name = "smith", NHSNumber = "a3" },
                new Patient { Id = 3, DateOfBirth = DateTime.Now, GPPractice = "GP4", Name = "felix", NHSNumber = "a4" },
                new Patient { Id = 4, DateOfBirth = DateTime.Now, GPPractice = "GP5", Name = "adam", NHSNumber = "a5" },
                new Patient { Id = 5, DateOfBirth = DateTime.Now, GPPractice = "GP6", Name = "james", NHSNumber = "a6" },
                new Patient { Id = 6, DateOfBirth = DateTime.Now, GPPractice = "GP7", Name = "paul", NHSNumber = "a7" },
                new Patient { Id = 7, DateOfBirth = DateTime.Now, GPPractice = "GP8", Name = "luke", NHSNumber = "a8" }
            );

            context.SaveChanges();
        }
    }
}
