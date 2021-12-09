using QRM4PB_HFT_2021221.Data;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Endpoint2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IRoomLogic, RoomLogic>();
            services.AddTransient<ICinemaLogic, CinemaLogic>();
            services.AddTransient<IMovieLogic, MovieLogic>();

            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<ICinemaRepository, CinemaRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();

            services.AddTransient<CinemaDbContext, CinemaDbContext>();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
