﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CleanCodeTest.Service
{
   public class CacheService
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

      public void SetUsingCache(string cacheKey, object body)
      {
         var elementJson = JsonConvert.SerializeObject(body);
         _cache.SetString(cacheKey, elementJson);
      }

      public string GetUsingCache(string cacheKey)
      {
         var elementJson = _cache.GetString(cacheKey);
         return elementJson;
      }

      public async Task DeleteFromCacheAsync(string cacheKey)
      {
         if (_redisEnabled)
            await _cache.RemoveAsync(cacheKey);
      }

      public void DeleteFromCache(string cacheKey)
      {
         if (_redisEnabled)
            _cache.RemoveAsync(cacheKey);
      }

   }
}