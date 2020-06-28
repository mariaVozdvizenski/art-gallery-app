using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.App;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Contracts.BLL.App.Mappers;
using Contracts.DAL.App;
using ee.itcollege.mavozd.Contracts.DAL.Base;using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MsSqlConnection")));
            
            services.AddIdentity<Domain.App.Identity.AppUser, Domain.App.Identity.AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IUserNameProvider, UserNameProvider>();
            services.AddScoped<IAppBLL, AppBLL>();
            services.AddScoped<IPaintingServiceMapper, PaintingServiceMapper>();

            
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddHttpContextAccessor();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            
            
            // =============== JWT support ===============
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    }; 
                });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                //options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.AssumeDefaultVersionWhenUnspecified = false;
            });
            
            services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,  IApiVersionDescriptionProvider provider)
        {
            UpdateDatabase(app, env, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsAllowAll");

            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach ( var description in provider.ApiVersionDescriptions )
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json"
                            ,
                            description.GroupName.ToUpperInvariant());
                    }
                } );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env,
            IConfiguration configuration)
        {
            // give me the scoped services (everyhting created by it will be closed at the end of service scope life).
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();

            if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
            {
                logger.LogInformation("DropDatabase");
                DataInitializers.DeleteDatabase(context);
            }

            if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
            {
                logger.LogInformation("MigrateDatabase");
                DataInitializers.MigrateDatabase(context);
            }

            if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
            {
                logger.LogInformation("SeedIdentity");
                DataInitializers.SeedIdentity(userManager, roleManager, context);
            }

            if (configuration.GetValue<bool>("DataInitialization:SeedData"))
            {
                logger.LogInformation("SeedData");
                DataInitializers.SeedData(context);
            }
        }
    }
}
