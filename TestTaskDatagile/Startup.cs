using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTaskDatagile.Redis;
using TestTaskDatagile.Services.Implementation;
using TestTaskDatagile.Services.Interface;
using Swashbuckle.AspNetCore.Swagger;
using System.Net.Http;
using Microsoft.OpenApi.Models;

namespace TestTaskDatagile
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
            services.AddHttpClient();

            services.AddControllers();

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api",
                Version = "v1",
                Description = "Contains api for working with dog.ceo and redis"
            }));

            // регистрация сервисов для обработки запросов апи
            services.AddSingleton<IDogeRequestProcessor, DogeRequestProcessor>();
            services.AddSingleton<IDogBreedListLoader, DogBreedListLoader>();
            services.AddSingleton<IImageLoader, ImageLoader>();
            services.AddSingleton<IRedisUploader, RedisUploader>();
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

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dogs Api V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
