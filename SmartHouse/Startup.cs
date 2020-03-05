using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AppServices.Services;
using AppServices.ChainOfResponsibility;
using Microsoft.Extensions.Logging;
using DataAccess.Context;
using DataAccess;
using Common.Services;
using AutoMapper;
using SmartHouse.Mapping;
using Microsoft.AspNetCore.Cors;
using System.Web.Http;

namespace SmartHouse
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
            services.AddDbContext<DataAccess.Context.ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options => options.AddPolicy("AllowLocalhost4200", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod())
           );


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<DataAccess.Context.ApplicationDbContext>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddTransient<IAppService, AppService>();


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IKeyService, KeyService>();

            services.AddTransient<IHandlerFactory<IMessageParameterHandler>, HandlerFactory<IMessageParameterHandler>>();
            
            services.AddTransient<IMessageParameterHandler, NextNameParameterHandler>();
            services.AddTransient<IMessageParameterHandler, NameParameterHandler>();

            services.AddTransient<IChainCreator<IChainHandler>, ChainCreator<IChainHandler>>();
            services.AddTransient<IChainHandler, MonkeyHandler>();
            services.AddTransient<IChainHandler, SquirrelHandler>();
            services.AddTransient<IChainHandler, DogHandler>();

            services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowLocalhost4200");

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

            loggerFactory.AddLog4Net();

            app.UseRouting();



            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
