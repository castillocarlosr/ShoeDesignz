using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();
            //Local Strings
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:LocalUserConnection"]));
            services.AddDbContext<ShoeDesignzDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //Deployment Strings
            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:IdentityDefaultConnection"]));
            //services.AddDbContext<ShoeDesignzDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:ProductionConnection"]));

            services.AddScoped<IInventory, InventoryManagementServices>();
            services.AddScoped<ICart, CartService>();
            services.AddScoped<IOrder, OrderService>();

            //This is the Authorization used for the Admin and the people with the edu email policy to access certain parts of the website only.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EduEmail", policy => policy.Requirements.Add(new EduEmailRequirement()));
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin));
            });

            services.AddScoped<IAuthorizationHandler, EduEmailRequirement>();

            services.AddScoped<IEmailSender, EmailSender>();
            
            //The Authentication key and secrets used to 3rd Party OAutho.  Twitter and Google coming soon.  
            services.AddAuthentication()
            //    .AddTwitter(twitterOptions =>
            //{
            //    twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //    twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //})
            //    .AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //})
                .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
                .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
            });
            
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
