using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RealEstateApi.HttpClients;
using RealEstateApi.Services;
using Refit;
using System;

namespace RealEstateApi
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
            services.AddControllers();

            services.AddSingleton<IPropertyService, PropertyService>();
            services.Configure<ZillowApiSettings>(Configuration.GetSection("Zillow"));

            var refitSettings = new RefitSettings { ContentSerializer = new XmlContentSerializer() };
            services.AddRefitClient<IZillowApi>(refitSettings)
                .ConfigureHttpClient((provider, client) =>
                {
                    var settings = provider.GetRequiredService<IOptions<ZillowApiSettings>>().Value;
                    client.BaseAddress = new Uri(settings.SiteUrl);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
