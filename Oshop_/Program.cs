
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Oshop.BLL.Services.Classes;
using Oshop.BLL.Services.Interfaces;
using Oshop.DAL.Data;
using Oshop.DAL.Model;
using Oshop.DAL.Repos.Classes;
using Oshop.DAL.Repos.Interfaces;
using Oshop.DAL.Utalities;
using Oshop.PL.utalities;
using Scalar;

using Scalar.AspNetCore;
using System.Text;
using System.Threading.Tasks;
namespace Oshop_
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped(typeof(IGenericReposetory<>), typeof(GenericReposetory<>));
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IBrandRepesotory, BrandReposotory>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();

            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ISeedData,SeedData>();
            builder.Services.AddScoped<IAuthService,AuthUser>();
            builder.Services.AddScoped<IEmailSender, SendEmail>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IProductService, ProductService>();




            //to make to all identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//to cinvert from cookies to token
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//to return 401 when not authorize
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                     };
                 });
                     var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            var scoap = app.Services.CreateScope();
           var objectOfSeed= scoap.ServiceProvider.GetRequiredService<ISeedData>();
           await objectOfSeed.DataSeedingAsync();
            await objectOfSeed.IdentityDataSeedingAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
