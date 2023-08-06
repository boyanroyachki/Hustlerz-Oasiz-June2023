using HustlerzOasiz.Services.Data.Interfaces;
using HustlerzOasiz.Web.Data;
using HustlerzOasiz.Web.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HustlerzOasiz.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<HustlerzOasizDbContext>(options =>
                options.UseSqlServer(connectionString));


            // Add services to the container.

            builder.Services.AddApplicationServices(typeof(IHomeService));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup => 
            {
                setup.AddPolicy("HustlerzOasiz", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7049/").AllowAnyHeader().AllowAnyMethod();
                });
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("HustlerzOasiz");

            app.Run();
        }
    }
}