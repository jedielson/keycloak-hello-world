using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(
                opt =>
                {
                    opt.Cookie.SameSite = SameSiteMode.None;
                    opt.Cookie.Name = "AuthCookie";
                    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    opt.SlidingExpiration = true;
                }
            )
            .AddOpenIdConnect(opt =>
            {
                opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.Authority = "http://localhost:8082/auth/realms/local";
                opt.ClientId = "mvc";
                opt.ClientSecret = "817312af-79b2-47f5-be81-3641e7245197";
                opt.ResponseType = OpenIdConnectResponseType.Code;

                opt.SaveTokens = true;
                opt.RequireHttpsMetadata = false;

                opt.Scope.Add("openid");
                opt.Scope.Add("profile");
            });

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    //.RequireAuthorization()
                    ;
            });
        }
    }
}
