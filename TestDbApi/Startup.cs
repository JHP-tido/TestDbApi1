using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using TestDbApi.Data;
using TestDbApi.Extensions;

namespace TestDbApi
{
    public class Startup
    {
        private readonly MySettings _mySettings;
        public IConfiguration Configuration { get; }
        //private readonly IOptions<MySettings> _mySettings;

        public Startup(IConfiguration configuration, IOptions<MySettings> options, IHostingEnvironment env)
        {
            _mySettings = options.Value;
            Configuration = configuration;  
            /*var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();*/
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //Added JWT service from ServiceExtension
            services.ConfigureJWTService(Configuration);
            services.ConfigureAuthorizationPolicy();
            services.Configure<MySettings>(Configuration.GetSection("mySettings"));
            MySettings mySettings = new MySettings();
            Configuration.GetSection("mySettings").Bind(mySettings);
            //services.Configure<MySettings>(Configuration.GetSection("mySettings"));
            //services.AddSingleton(Configuration.GetSection("mySettings").Get<MySettings>());
            //services.AddSingleton(Configuration); 
            //services.AddSingleton(cfg => cfg.GetService<IOptions<MySettings>>().Value);
            services.ConfigureRepositoryWrapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Call Dbstring here
            var connection = mySettings.DbString;
            services.AddDbContext<TheCRMContext>
                (options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
