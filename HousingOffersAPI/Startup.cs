using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Entities;
using HousingOffersAPI.Services;
using HousingOffersAPI.Services.UsersRelated;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HousingOffersAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=HousingOffersDB;Trusted_Connection=True;";
            services.AddDbContext<HousingOffersContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IOffersRepozitory, OffersRepozitory>();
            services.AddScoped<IUsersRepozitory, UsersRepozitory>();
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
