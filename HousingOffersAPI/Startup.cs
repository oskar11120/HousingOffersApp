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
using AutoMapper;
using HousingOffersAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HousingOffersAPI.Services.Validators;

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

            services.AddDbContext<HousingOffersContext>(o => o.UseSqlServer(Configuration["ConnectionStrings:HousingOffersDB"]));

            services.AddScoped<IOffersRepozitory, OffersRepozitory>();
            services.AddScoped<IUsersRepozitory, UsersRepozitory>();

            services.AddScoped<IJwtManager, JwtManager>();

            services.AddSingleton<IUserValidator, UserValidator>();

            services.Configure<List<string>>(Configuration.GetSection("SecurityKeys"));

            //security
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["SecurityKeys:0"]))
                };
            });
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

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Offer, Models.OfferModel>();
                cfg.CreateMap<Entities.User, Models.UserModel>();
                cfg.CreateMap<Entities.ImageAdress, Models.ImageAdressModel>();
                cfg.CreateMap<Entities.OfferTag, Models.OfferTagModel>();

                cfg.CreateMap<Models.OfferModel, Entities.Offer>();
                cfg.CreateMap<Models.UserModel, Entities.User>();
                cfg.CreateMap<Models.ImageAdressModel, Entities.ImageAdress>();
                cfg.CreateMap<Models.OfferTagModel, Entities.OfferTag>();
            });

            app.UseMvc();
        }
    }
}
