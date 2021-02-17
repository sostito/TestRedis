using CleanCodeTest.Service;
using CleanCodeTest.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanCodeTest
{
   public class Startup
   {
      readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddDistributedMemoryCache();
         services.AddAuthentication();

         //Services
         services.AddScoped<IRedisService, RedisService>();
         services.AddScoped<CacheService, CacheService>();

         services.AddCors(options =>
         {
            options.AddPolicy(MyAllowSpecificOrigins,
               builder =>
               {
                  builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
               });
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

         app.UseCors(MyAllowSpecificOrigins);

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
