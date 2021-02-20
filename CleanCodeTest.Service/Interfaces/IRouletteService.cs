using CleanCodeTest.Model;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using System.Threading.Tasks;

namespace CleanCodeTest.Service.Interfaces
{
   public interface IRouletteService
   {
      Task<GeneralResponse> CreateRoulette();
      Task<GeneralResponse> OpeningRoulette(RouletteOpeningRequest request);
      Task<GeneralResponse> MakeBet(MakeBetRequest request);
      Task<GeneralResponse> CloseBets(CloseBetsRequest request);
      Task<GeneralResponse> GetRouletteList();
   }
}
