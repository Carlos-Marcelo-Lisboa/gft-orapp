using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantOrderApp.BLL;
using RestaurantOrderApp.DAL;
using RestaurantOrderApp.Interface;
using RestaurantOrderApp.Repositoriy;

namespace RestaurantOrderApp.API
{
    public class Startup
    {

        readonly string allowedOriginsPoliceName = "AllowedOrigens";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy(name: allowedOriginsPoliceName,
                                  builder =>
                                  {
                                      builder.WithOrigins(Configuration["AllowedOrigens"].Split(','))
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials();
                                  });
            });

            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<RestaurantOrderAppContext>();
            services.AddTransient<IOrder, Order>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(allowedOriginsPoliceName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
