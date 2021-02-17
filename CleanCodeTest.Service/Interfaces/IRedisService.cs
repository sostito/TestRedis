using CleanCodeTest.Model;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;

namespace CleanCodeTest.Service.Interfaces
{
   public interface IRedisService
   {
      GeneralResponse CreateRoulette();
      GeneralResponse RouletteOpening(RouletteOpeningRequest request);
      GeneralResponse MakeBet(MakeBetRequest request);
      GeneralResponse CloseBets(CloseBetsRequest request);
      GeneralResponse GetRouletteList();
   }
}
