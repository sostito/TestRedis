using CleanCodeTest.Service.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CleanCodeTest.Service
{
   public class CacheService : ICacheService
   {
      private readonly IDistributedCache _cache;
      private readonly IConfiguration _configuration;
      private bool _redisEnabled;

      public CacheService(IDistributedCache cache, IConfiguration configuration)
      {
         _cache = cache;
         _configuration = configuration;
         _redisEnabled = bool.Parse(_configuration.GetSection("RedisCache").GetSection("Enabled").Value);
      }

      public async Task SetUsingCache(string cacheKey, object body)
      {
         var elementJson = JsonConvert.SerializeObject(body);
         if (_redisEnabled)
            await _cache.SetStringAsync(cacheKey, elementJson);
      }

      public async Task<string> GetUsingCache(string cacheKey)
      {
         string elementJson = string.Empty;
         if (_redisEnabled)
            elementJson = await _cache.GetStringAsync(cacheKey);
         return elementJson;
      }
   }
}
