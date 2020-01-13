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
using HousingOffersAPI.Options;
using HousingOffersAPI.Services.AnalyticsRelated;

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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<HousingOffersContext>(o => o.UseSqlServer(Configuration["ApiOptions:ConnectionStrings:HousingOffersDB"]));

            services.AddScoped<IOffersRepozitory, OffersRepozitory>();
            services.AddScoped<IUsersRepozitory, UsersRepozitory>();
            services.AddScoped<IAnalyticsDataRepozitory, AnalyticsDataRepozitory>();

            services.AddScoped<IJwtManager, JwtManager>();

            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IOfferValidator, OfferValidator>();
            services.AddScoped<IOfferGetRequestValidator, OfferGetRequestValidator>();

            //services.Configure<List<string>>(Configuration.GetSection("SecurityKeys"));
            //services.Configure<Dictionary<string, List<string>>>(Configuration.GetSection("AllowedValues"));

            services.Configure<ApiOptions>(Configuration.GetSection("ApiOptions"));

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
                        Encoding.UTF8.GetBytes(Configuration["ApiOptions:UsersControllerOptions:SecurityKeys:JWT"]))
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

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseHttpsRedirection();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Offer, Models.OfferModel>();
                cfg.CreateMap<Entities.User, Models.UserModel>();
                cfg.CreateMap<Entities.ImageAdress, Models.ImageAdressModel>();
                cfg.CreateMap<Entities.OfferTag, Models.OfferTagModel>();
                cfg.CreateMap<Entities.Location, Models.DatabaseRelated.LocationModel>();

                cfg.CreateMap<Models.OfferModel, Entities.Offer>();
                cfg.CreateMap<Models.UserModel, Entities.User>();
                cfg.CreateMap<Models.ImageAdressModel, Entities.ImageAdress>();
                cfg.CreateMap<Models.OfferTagModel, Entities.OfferTag>();
                cfg.CreateMap<Models.DatabaseRelated.LocationModel, Entities.Location>();
            });

            app.UseMvc();
        }
    }
}
