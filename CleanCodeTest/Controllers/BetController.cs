using CleanCodeTest.Common;
using CleanCodeTest.Model;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using CleanCodeTest.Service;
using CleanCodeTest.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanCodeTest.General.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class BetController : ControllerBase
   {
      readonly IRedisService redisService;
      readonly CacheService cacheService;
      public BetController(IRedisService redisService, CacheService cacheService)
      {
         this.redisService = redisService;
         this.cacheService = cacheService;
      }

      [HttpPost("[action]")]
      public IActionResult CreateRoulette()
      {
         GeneralResponse response = redisService.CreateRoulette();
         return Ok(response);
      }

      [HttpPost("[action]")]
      public IActionResult RouletteOpening(RouletteOpeningRequest rouletteOpeningRequest)
      {
         GeneralResponse response = redisService.RouletteOpening(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost("[action]")]
      public IActionResult MakeBet(MakeBetRequest rouletteOpeningRequest)
      {
         if (!Request.Headers.ContainsKey("UserId"))
            return BadRequest(Constants.ERROR_HEADER_IDUSUARIO);

         rouletteOpeningRequest.UserId = Request.Headers["UserId"];
         GeneralResponse response = redisService.MakeBet(rouletteOpeningRequest);
         return Ok(response);
      }

      [HttpPost("[action]")]
      public IActionResult CloseBets(CloseBetsRequest closeBetsRequest)
      {
         GeneralResponse response = redisService.CloseBets(closeBetsRequest);
         return Ok(response);
      }

      [HttpGet("[action]")]
      public IActionResult GetRouletteList()
      {
         GeneralResponse response = redisService.GetRouletteList();
         return Ok(response);
      }

   }
}
