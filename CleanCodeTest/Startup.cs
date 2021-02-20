using CleanCodeTest.Service;
using CleanCodeTest.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

         services.AddScoped<IRouletteService, RouletteService>();
         services.AddScoped<ICacheService, CacheService>();

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
