using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MTR.Repository;
using MTR.Services;

using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace MTR
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Mobile Tax Register Application Programming Interface (MTR-API)",
                    Description = "Kenya Revenue Authority Compliant Application Programming Interface (API) developed by Soluhisho Software Solutions Limited",
                    TermsOfService = new Uri("https://www.businessdailyafrica.com/bd/economy/-kra-to-track-traders-daily-sales-in-new-law-2463274"),

                    ////Contact = new OpenApiContact
                    ////{
                    ////    Name = "Leakey maina",
                    ////    Email = "LeakeyMaina@Soluhisho.co.ke",
                    ////    Url = new Uri("https://soluhisho.co.ke"),
                    ////},
                    ////License = new OpenApiLicense
                    ////{
                    ////    Name = "Use under LICX",
                    ////    Url = new Uri("https://example.com/license"),
                    ////}
                });
                //// Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            //services.AddDbContext<MTRDbContext>(opt =>
            //                                    opt.UseSqlServer(Configuration.GetConnectionString("MTRDBLocal")));


            services.AddDbContext<MTRDbContext>(opt =>
                                                opt.UseSqlServer(Configuration.GetConnectionString("MTRDBAzure")));

            //var server = Configuration["DBserver"] ?? "localhost";
            //var port = Configuration["DBPort"] ?? "1433";
            ////var user = Configuration["DBUser"] ?? "MTRAPI";
            //var user = Configuration["DBUser"] ?? "SA";
            //var password = Configuration["DBPassword"] ?? "Liki@Soluhisho";
            //var database = Configuration["Database"] ?? "MTR";


            //services.AddDbContext<MTRDbContext>(opt =>
            //                                    opt.UseSqlServer(
            //                                        $"Server={server},{port}; " +
            //                                        $"initial catalog={database}; " +
            //                                        $"User ID={user};Password={password}"
            //                                        ));


            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IETRRepository, SQLRepository>();
            services.AddSingleton<ISMSService, TwilioService>();
            services.AddSingleton<IRandomCodeGenerator, RandomCodeGenerator>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
