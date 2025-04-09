using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HotelGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            
            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            /*builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API Gateway", Version = "v1" });
            });*/
            //builder.Services.AddOcelot();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });

                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Gateway",
                    Version = "v1"
                });
            });

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot();   
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var app = builder.Build();

            //app.UseCors("AllowAllOrigins");
            app.UseCors("AllowAngularApp");
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            
            app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
                    c.SwaggerEndpoint("/swagger/authservice.json", "AuthService API");


                    c.EnableDeepLinking();
                    c.DisplayOperationId();
                });
            app.UseCors("AllowAll");
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[Gateway] Incoming: {context.Request.Method} {context.Request.Path}");
                await next();
            });
            // app.UseHttpsRedirection();
            app.MapGet("/", () => "Gateway is working");
            app.UseAuthorization();
            app.UseOcelot();

            app.Run();
        }
    }
}
