using BusinessWebSoftPvtLmtTaskApi.Data;
using BusinessWebSoftPvtLmtTaskApi.Repository.IRepository;
using BusinessWebSoftPvtLmtTaskApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace BusinessWebSoftPvtLmtTaskApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            var configuration = builder.Configuration;
            string cs = configuration.GetConnectionString("conStr");

            // Register ApplicationDbContext with the connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(cs));
            builder.Services.AddScoped<IGenericRepository, GenericRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}