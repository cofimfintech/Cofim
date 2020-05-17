using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cofim.Web.Helpers;
using Cofim.Web.Models;
using Cofim.Web.Models.Entities;
using Cofim.Web.Models.InitDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cofim.Common.Services;

namespace Cofim.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded    = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath        = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });

            services.AddIdentity<UserLogin, IdentityRole>(cfg => {
                        cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                        cfg.SignIn.RequireConfirmedEmail      = true;
                        cfg.User.RequireUniqueEmail           = true;
                        cfg.Password.RequireDigit             = false;
                        cfg.Password.RequireLowercase         = false;
                        cfg.Password.RequireNonAlphanumeric   = false;
                        cfg.Password.RequireUppercase         = false;
                        cfg.Password.RequiredUniqueChars      = 0;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddCookie()
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer   = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer      = Configuration["CofimSettings:JwtIssuer"],
                        ValidAudience    = Configuration["CofimSettings:Jwtaudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TramiAutoSettings:JwtSecretKey"])),
                        ClockSkew        = TimeSpan.Zero
                    });

            /*** INYECCION DE DEPENDENCIAS tres tipo: Transient(Solo se ejecuta una sola vez), singleton (carga en memoria se mantiene), scope (se inyecta cada vez que se necesita, crea una nueva instancia) ***/
            services.AddDbContext<DataContext>(cfg => { cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });
            services.AddScoped<IUserHelper  , UserHelper>();
            services.AddScoped<IMailHelper  , MailHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IMenuService , MenuService>();
            services.AddScoped<IEtlHelper   , EtlHelper>();
            services.AddTransient<SeedDb>();
            /*** INYECCION DE DEPENDENCIAS ***/

            services.AddControllersWithViews();
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

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU4MzMyQDMxMzgyZTMxMmUzME9PUkFzVVloTFc3cTd0T09pMmlTbGRQUVN2ekpSWG10bXJiL253ZlExYms9");


            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); });
        }
    }//Startup
}
