using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenId;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roulette.Context;
using Roulette.Controllers;
using Roulette.Models;

namespace Roulette
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddServerSideBlazor();
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                })
                .AddSteam(options => { options.Events.OnAuthenticated += OnClientAuthenticated; });

            services.AddMvc();
            services.AddTransient<SteamUsersController, SteamUsersController>();
            services.AddScoped<HomeModel>();
            services.AddHttpClient();
            services.AddDbContext<AppDbContext>(options => options.UseMySql(
                "server=localhost;user=root;password=qwer1234;database=roulette;",
                new MySqlServerVersion(new Version(8, 0, 22))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapBlazorHub();
            });
        }

        private async Task OnClientAuthenticated(OpenIdAuthenticatedContext context)
        {
            var userManager = context.HttpContext.RequestServices.GetService<SteamUsersController>();

            var steamId = ulong.Parse(new Uri(context.Identifier).Segments.Last());

            if (userManager != null && !await userManager.IsRegisteredAsync(steamId.ToString()))
                await userManager.RegisterUserAsync(steamId.ToString());

            context.Identity.AddClaim(new Claim("steamID", steamId.ToString()));
            var identity = new ClaimsPrincipal(context.Identity);


            context.HttpContext.User = identity;
        }
    }
}