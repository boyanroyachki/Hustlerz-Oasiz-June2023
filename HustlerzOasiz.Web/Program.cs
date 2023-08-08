using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Services.Mapping;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.Infrastructure.CustomModelBinders;
using HustlerzOasiz.Web.Infrastructure.Extensions;
using HustlerzOasiz.Web.ViewModels.Home;
using MarauderzOasiz.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

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


            }).AddRoles<IdentityRole<Guid>>() //this is the most important for roles, after adding this line you can add roles trough MSSMS if you wish
                .AddEntityFrameworkStores<HustlerzOasizDbContext>();
            //
            builder.Services.AddApplicationServices(typeof(IJobService));

            builder.Services.AddRecaptchaService(); //just for a testing

            //
            builder.Services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomDecimalBinderProvider());
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); //cybersec
            });

            

            WebApplication app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

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

            app.SeedAdmin(DevelopmentAdminEmail); //make admin by const emain in HustlerzOasiz.Common/GeneralApplicationConstants;

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "ProtectingUrlPattern", 
                    pattern: "/{controller}/{action}/{id}/{information}"
                    );
                config.MapDefaultControllerRoute();   //could add custom route here
                config.MapRazorPages();

            });

            app.Run();
        }
    }
}