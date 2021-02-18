using CleanCodeTest.Common;
using CleanCodeTest.Model;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using CleanCodeTest.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanCodeTest.General.Controllers
{
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class BetController : ControllerBase
   {
      readonly IRouletteService rouletteService;
      public BetController(IRouletteService rouletteService)
      {
         this.rouletteService = rouletteService;
      }

      [HttpPost]
      public async Task<IActionResult> CreateRoulette()
      {
         GeneralResponse response = await rouletteService.CreateRoulette();
         return Ok(response);
      }

      [HttpPost]
      public async Task<IActionResult> OpeningRoulette(RouletteOpeningRequest rouletteOpeningRequest)
      {
         GeneralResponse response = await rouletteService.OpeningRoulette(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost]
      public async Task<IActionResult> MakeBet(MakeBetRequest rouletteOpeningRequest)
      {
         if (!Request.Headers.ContainsKey("UserId"))
            return BadRequest(Constants.ERROR_HEADER_IDUSUARIO);

         rouletteOpeningRequest.UserId = Request.Headers["UserId"];
         GeneralResponse response = await rouletteService.MakeBet(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost]
      public async Task<IActionResult> CloseBets(CloseBetsRequest closeBetsRequest)
      {
         GeneralResponse response = await rouletteService.CloseBets(closeBetsRequest);
         return Ok(response);
      }

      [HttpGet]
      public async Task<IActionResult> GetRouletteList()
      {
         GeneralResponse response = await rouletteService.GetRouletteList();
         return Ok(response);
      }

   }
}
