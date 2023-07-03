using ApiMovimientos.Config;
using ApiMovimientos.Contracts;
using ApiMovimientos.Filters;
using ApiMovimientos.Repository;
using ApiMovimientos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiMovimientos
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

            
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.Configure<DbSettings>(Configuration.GetSection("ProductDatabase"));
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));

            var redisSettings = Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisSettings.ToConnectionString();
            });


            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = redisSettings.ToConnectionString();
            //    options.InstanceName = redisSettings.Name;
            //    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
            //    {
            //        SyncTimeout = redisSettings.SyncTimeout // Valor válido para SyncTimeout (en milisegundos)
            //    };
            //});

            //var serializerSettings = new JsonSerializerSettings();

            //AddSingletonServiceCache<ProductDetails>(services, serializerSettings);

            //services.AddSingleton<IReadOnlyPolicyRegistry<string>, PolicyRegistry>((serviceProvider) =>
            //{
            //    PolicyRegistry registry = new();
            //    registry.Add(typeof(ProductDetails).ToString(), Policy.CacheAsync(serviceProvider.GetRequiredService<IAsyncCacheProvider<ProductDetails>>(), TimeSpan.FromMinutes(15)));

            //    return registry;
            //});

            services.AddControllers( opt =>
            {
                opt.Filters.Add<HttpResponseExceptionFilter>();

            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMovimientos", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiMovimientos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddSingletonServiceCache<T>(IServiceCollection services, JsonSerializerSettings serializerSettings)
        {
            //services.AddSingleton<IAsyncCacheProvider<T>>(serviceProvider =>
            //    serviceProvider
            //        .GetRequiredService<IDistributedCache>()
            //        .AsAsyncCacheProvider<string>()
            //        .WithSerializer<T, string>(
            //            new Polly.Caching.Serialization.Json.JsonSerializer<T>(serializerSettings)
            //        )
            //    );

            //services.AddSingleton(x => {
            //    var policyRegistry = x.GetRequiredService<IReadOnlyPolicyRegistry<string>>();
            //    return policyRegistry.Get<IAsyncPolicy<T>>(typeof(T).ToString());
            //});
        }

    }
}
