
using Microsoft.EntityFrameworkCore;
using Oshop.BLL.Services.Classes;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.Data;
using Oshop.DAL.Repos.Classes;
using Oshop.DAL.Repos.Interfaces;
using Scalar;

using Scalar.AspNetCore;
namespace Oshop_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped(typeof(IGenericReposetory<>), typeof(GenericReposetory<>));
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBrandRepesotory, BrandReposotory>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
