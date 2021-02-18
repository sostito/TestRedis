using System.Threading.Tasks;

namespace CleanCodeTest.Service.Interfaces
{
   public interface ICacheService
   {
      Task SetUsingCache(string cacheKey, object body);
      Task<string> GetUsingCache(string cacheKey);
   }
}
