using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Services.Data;
using HustlerzOasiz.Web.Data;
using MarauderzOasiz.Data.Models;
using Microsoft.EntityFrameworkCore;
using HustlerzOasiz.Web.Infrastructure.Extensions;
using HustlerzOasiz.Web.Infrastructure.CustomModelBinders;
using Microsoft.AspNetCore.Builder;

namespace HustlerzOasiz.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<HustlerzOasizDbContext>(options =>
                options.UseSqlServer(connectionString));

            

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");

                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");


            })
                .AddEntityFrameworkStores<HustlerzOasizDbContext>();
            //
            builder.Services.AddApplicationServices(typeof(IJobService));

            //
            builder.Services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomDecimalBinderProvider());
            });
            

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}"); //all that is needed 
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  //1st
            app.UseAuthorization();  //2st

            app.MapDefaultControllerRoute(); //could add custom route here
            app.MapRazorPages();

            app.Run();
        }
    }
}