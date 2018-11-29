﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Company_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Company_API
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

            var connection = @"Server=tcp:inspiritosql.database.windows.net,1433;Initial Catalog=norwayerp_db;Persist Security Info=False;User ID=backend.dev.sadmin;Password=Welcome!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //var connection = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=NorwayERP;Integrated Security=SSPI;";
            services.AddDbContext<CompanyDbContext>
                (options => options.UseSqlServer(connection));

            //This line adds Swagger generation services to our container.
            services.AddSwaggerGen(c =>
            {
                //The generated Swagger JSON file will have these properties.
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Company demo v1",
                    Version = "v1",
                });

                //c.ResolveConflictingActions(apiDescription => apiDescription.Where(x => x.RelativePath.Contains("parameterName")).FirstOrDefault());
                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //This line enables the app to use Swagger, with the configuration in the ConfigureServices method.
            app.UseSwagger();

            //This line enables Swagger UI, which provides us with a nice, simple UI with which we can view our API calls.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company demo v1");
            });
        }
    }
}
