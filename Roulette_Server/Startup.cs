using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roulette.Context;
using Roulette.Controllers;

namespace Roulette_Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
            services.AddSignalR();
            services.AddControllers();
            services.AddScoped<DBController>();
            services.AddScoped<NotificationHub>();
            services.AddDbContext<AppDbContext>(options => options.UseMySql(
                Roulette.Program.ConnectionString,
                new MySqlServerVersion(new Version(8, 0, 22))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationhub");
                endpoints.MapControllers();
            });
        }
    }
}