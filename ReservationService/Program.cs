using ReservationService.Repositories;
using ReservationService.Interface;
using ReservationService.Models;
using Microsoft.EntityFrameworkCore;
using ReservationService.Repository;

namespace ReservationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ReservationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

          
            builder.Services.AddScoped<IReservation, ReservationRepository>();
            builder.Services.AddScoped<IRoom, RoomRepository>();
            builder.Services.AddScoped<IGuest, GuestRepository>();
            // Add CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin() // Allow requests from any origin (CORS)
                          .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
                          .AllowAnyHeader()); // Allow any headers
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            app.UseCors("AllowAll"); 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
