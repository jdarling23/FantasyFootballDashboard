using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using FantasyFootballDashboard.Service.Interfaces;
using FantasyFootballDashboard.Service;
using Microsoft.EntityFrameworkCore;
using FantasyFootbalDashboard.DBConnector;
using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootbalDashboard.DBConnector.Repositories;
using FantasyFootballDashboard.APIConnector.SportsData;

namespace FatnasyFootballDashboard.API
{
    /// <summary>
    /// Initializes needed services on startup of the app
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary> 
        /// <param name="app">Service container, provided by runtime</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Security Configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            // Logging Configuration
            services.AddLogging();
            services.AddApplicationInsightsTelemetry();

            // API Configuration
            services.AddControllers().AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "Fantasy Football Dashboard API",
                    Version = "v1.0",
                    Description = "API used to provide access to all of your fantasy football players across services in one place.",
                    Contact = new OpenApiContact
                    {
                        Name = "James Darling",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jdarling23/FantasyFootballDashboard"),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });
            });

            // Dependency Injection
            services.AddDbContext<FantasyFootballDashboardContext>(c => 
            {
                c.UseSqlServer(Configuration["DatabaseConnectionString"]);
            });

            services.AddTransient<IReferencePlayerRepository, ReferencePlayerRepository>();

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IDataService, DataService>(s => 
            {
                var sportsDataConn = new SportsDataConnector(Configuration["SportsDataApiKey"]);
                return new DataService(sportsDataConn, s.GetService<IReferencePlayerRepository>());
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder, provided by runtime</param>
        /// <param name="env">Environment settings, provided by runtime</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Fantasy Football Dashboard API V1.0");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
