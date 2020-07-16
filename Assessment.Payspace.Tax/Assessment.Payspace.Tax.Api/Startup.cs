using Assessment.Payspace.Tax.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Interface.Logic;
using Assessment.Payspace.Tax.Logic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ITaxLogic, TaxLogic>();
            services.AddSingleton<ITaxDataAccess, TaxDataAccess>();

            services.AddHttpClient();
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Tax Assessment Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           var corsOriginSources = Configuration.GetSection("Tokens:CorsOrigins").Get<List<string>>();

            app.UseRouting();

            app.UseCors(options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(corsOriginSources.ToArray());

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Tax Assessment Api V1");
            });
        }
    }
}
