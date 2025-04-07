using EmployeeService.Models;
using EmployeeService.Repositories;
using EmployeeService.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeContext")));

            builder.Services.AddScoped<IEmployee, EmployeeRepository>();
            builder.Services.AddScoped<IDepartment, DepartmentRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin() // Allow requests from any origin (CORS)
                          .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
                          .AllowAnyHeader()); // Allow any headers
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewOnly", policy =>
                    policy.RequireRole("Admin", "Manager", "Receptionist"));

                options.AddPolicy("ManageStaff", policy =>
                    policy.RequireRole("Admin", "Manager"));

                options.AddPolicy("ManageDepartments", policy =>
                    policy.RequireRole("Admin"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
