
using E_Commerce2Business_V01.Interfaces;
using E_Commerce3APIs_V01.EnpointsHelper;
using E_Commerce3APIs_V01.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E_Commerce3APIs_V01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCustomConfigurations(builder.Configuration);
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<ExceptionHandlingMiddleware>();

            //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapEndpoints();
            
            app.MapGet("test/test", (IBasketItemService service) =>
            {
                  service.TestAsync();
            });

            app.Run();
        }
    }
}
