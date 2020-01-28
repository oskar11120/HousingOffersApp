using System;
using HousingOffersAPI.Entities;
using HousingOffersAPI.Services;
using HousingOffersAPI.Services.UsersRelated;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HousingOffersAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HousingOffersAPI.Services.Validators;
using HousingOffersAPI.Options;
using HousingOffersAPI.Services.AnalyticsRelated;
using HousingOffersAPI.Services.TaskRelated;
using HousingOffersAPI.Services.ScriptRelated;
using HousingOffersAPI.Services.RecommendationRelated;
using HousingOffersAPI.Models.AnalyticsRelated;
using HousingOffersAPI.Services.ClicksRelated;

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
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IClicksRepository, ClicksRepository>();

            services.AddScoped<IJwtManager, JwtManager>();

            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IOfferValidator, OfferValidator>();
            services.AddScoped<IOfferGetRequestValidator, OfferGetRequestValidator>();          

            services.Configure<ApiOptions>(Configuration.GetSection("ApiOptions"));

            services.AddSingleton<IBackgroundTaskScheduler, BackgroundTaskScheduler>();
            services.AddSingleton<IRScriptTasksRunner>(service => new RScriptTasksRunner(Configuration["ApiOptions:ConnectionStrings:HousingOffersDB"]));

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackgroundTaskScheduler backgroundTaskScheduler, IRScriptTasksRunner rScriptTasksRunner)
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
                cfg.CreateMap<UserClick, UserClickModel>();
                cfg.CreateMap<OfferClick, OfferClickModel>();

                cfg.CreateMap<Models.OfferModel, Entities.Offer>();
                cfg.CreateMap<Models.UserModel, Entities.User>();
                cfg.CreateMap<Models.ImageAdressModel, Entities.ImageAdress>();
                cfg.CreateMap<Models.OfferTagModel, Entities.OfferTag>();
                cfg.CreateMap<Models.DatabaseRelated.LocationModel, Entities.Location>();
                cfg.CreateMap<UserClickModel, UserClick>();
                cfg.CreateMap<OfferClickModel, OfferClick>();

            });

            app.UseMvc();

            backgroundTaskScheduler.Schedule(
                () => rScriptTasksRunner.UpdateScriptCsvs(),
                new TimeSpan(0, 60, 0)
                );

            backgroundTaskScheduler.Schedule(
                () => rScriptTasksRunner.RunRScript(Configuration["ApiOptions:RecomendationOptions:RecomendationScriptPath"]),
                new TimeSpan(0, 0, 15)
                );

        }
    }
}
