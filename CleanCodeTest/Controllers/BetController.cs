using CleanCodeTest.Common;
using CleanCodeTest.Model;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using CleanCodeTest.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
      public IActionResult CreateRoulette()
      {
         GeneralResponse response = rouletteService.CreateRoulette();
         return Ok(response);
      }

      [HttpPost]
      public IActionResult OpeningRoulette(RouletteOpeningRequest rouletteOpeningRequest)
      {
         GeneralResponse response = rouletteService.OpeningRoulette(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost]
      public IActionResult MakeBet(MakeBetRequest rouletteOpeningRequest)
      {
         if (!Request.Headers.ContainsKey("UserId"))
            return BadRequest(Constants.ERROR_HEADER_IDUSUARIO);

         rouletteOpeningRequest.UserId = Request.Headers["UserId"];
         GeneralResponse response = rouletteService.MakeBet(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost]
      public IActionResult CloseBets(CloseBetsRequest closeBetsRequest)
      {
         GeneralResponse response = rouletteService.CloseBets(closeBetsRequest);
         return Ok(response);
      }

      [HttpGet]
      public IActionResult GetRouletteList()
      {
         GeneralResponse response = rouletteService.GetRouletteList();
         return Ok(response);
      }

   }
}
