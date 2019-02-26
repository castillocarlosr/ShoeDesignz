using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoeDesignz.Data;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Handlers;
using ShoeDesignz.Models.Interfaces;
using ShoeDesignz.Models.Services;

namespace ShoeDesignz
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration) //IConfiguration configuration
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

           // services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("IdentityDefaultConnection")));

           services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("IdentityDefaultConnection")));
           //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<ShoeDesignzDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddDbContext<ShoeDesignzDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:ProductionConnection"]));


            //services.AddScoped<IInventory, InventoryManagementServices>();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("EduEmail", policy => policy.Requirements.Add(new EduEmailRequirement()));
                //options.AddPolicy("EduEmail", policy => policy.Requirements.Add(new EduEmailRequirement(".edu")));
            });

            services.AddScoped<IAuthorizationHandler, EduEmailRequirement>();

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("RiskTaker", policy => policy.Requirements.Add(new RiskTaker()));
                options.AddPolicy("RiskTaker", policy => policy.Requirements.Add(new RiskTaker("true")));
            });
            
            services.AddScoped<IAuthorizationHandler, RiskTaker>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
           

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
