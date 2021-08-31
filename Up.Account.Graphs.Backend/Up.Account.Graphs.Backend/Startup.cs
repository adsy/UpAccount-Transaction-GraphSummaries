using Domain.Config.ApiSettings;
using Infrastructure.Interface;
using Infrastructure.Interface.Repository;
using Infrastructure.Repository;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Up.Account.Graphs.Backend
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
            services.AddCors(options =>
              options.AddPolicy("Dev", builder =>
              {
                  // Allow multiple methods
                  builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
                    .WithHeaders(
                      HeaderNames.Accept,
                      HeaderNames.ContentType,
                      HeaderNames.Authorization)
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrWhiteSpace(origin)) return false;
                        // Only add this to allow testing with localhost, remove this line in production!
                        if (origin.ToLower().StartsWith("http://localhost")) return true;
                        // Insert your production domain here.
                        if (origin.ToLower().StartsWith("not made yet")) return true;
                        return false;
                    });
              })
            );

            services.Configure<UpApiSettings>(Configuration.GetSection(UpApiSettings.UpApiSettingsKey));

            services.AddHttpClient<IUpAccountRepository, UpAccountRepository>();

            services.AddTransient<IUpAccountService, UpAccountService>();

            services.AddTransient<IUpAccountRepository, UpAccountRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Up.Account.Graphs.Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Up.Account.Graphs.Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Dev");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}