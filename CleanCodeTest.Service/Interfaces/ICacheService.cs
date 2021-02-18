namespace CleanCodeTest.Service.Interfaces
{
   public interface ICacheService
   {
      void SetUsingCache(string cacheKey, object body);
      string GetUsingCache(string cacheKey);
      void DeleteFromCache(string cacheKey);
   }
}
