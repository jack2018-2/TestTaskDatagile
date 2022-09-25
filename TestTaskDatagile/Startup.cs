using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTaskDatagile.Redis;
using TestTaskDatagile.Services.Implementation;
using TestTaskDatagile.Services.Interface;

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

            services.AddControllers();

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
        }
    }
}
