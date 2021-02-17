using CleanCodeTest.Common;
using CleanCodeTest.Model;
using CleanCodeTest.Model.Dto;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using CleanCodeTest.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CleanCodeTest.Service
{
   public class RedisService : IRedisService
   {
      readonly CacheService cacheService;
      public RedisService(CacheService cacheService)
      {
         this.cacheService = cacheService;
      }
      public GeneralResponse CreateRoulette()
      {
         try
         {
            RouletteDTO rouletteDto = new RouletteDTO();
            rouletteDto.RouletteId = new Random().Next(0, 999999999);

            string Roulette = cacheService.GetUsingCache("Roulette");
            RouletteModel rouletteModel = new RouletteModel();
            if (!String.IsNullOrEmpty(Roulette))
               rouletteModel = JsonConvert.DeserializeObject<RouletteModel>(cacheService.GetUsingCache("Roulette"));

            rouletteModel.Roulette.Add(rouletteDto);

            cacheService.SetUsingCache("Roulette", rouletteModel);
            GeneralResponse generalResponse = new GeneralResponse(Constants.CREACION_RULETA_EXITOSA, rouletteDto.RouletteId);

            return generalResponse;
         }
         catch (Exception ex)
         {
            return new GeneralResponse(false);
         }
      }

      public GeneralResponse RouletteOpening(RouletteOpeningRequest request)
      {
         try
         {
            string Roulette = cacheService.GetUsingCache("Roulette");
            RouletteModel rouletteModel = new RouletteModel();
            if (!String.IsNullOrEmpty(Roulette))
               rouletteModel = JsonConvert.DeserializeObject<RouletteModel>(cacheService.GetUsingCache("Roulette"));
            else
               return new GeneralResponse(false);

            var elementIndex = rouletteModel.Roulette.FindIndex(t => t.RouletteId == request.RouletteId);
            var newItem = rouletteModel.Roulette[elementIndex];
            newItem.Enabled = true;
            rouletteModel.Roulette[elementIndex] = newItem;
            cacheService.SetUsingCache("Roulette", rouletteModel);
            GeneralResponse generalResponse = new GeneralResponse(Constants.APERTURA_RULETA_EXITOSA);

            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false);
         }
      }

      public GeneralResponse MakeBet(MakeBetRequest request)
      {
         try
         {
            string Roulette = cacheService.GetUsingCache("Roulette");
            RouletteModel rouletteModel = new RouletteModel();
            if (!String.IsNullOrEmpty(Roulette))
               rouletteModel = JsonConvert.DeserializeObject<RouletteModel>(cacheService.GetUsingCache("Roulette"));
            else
               return new GeneralResponse(false);

            var elementIndex = rouletteModel.Roulette.FindIndex(t => t.RouletteId == request.RouletteId && t.Enabled);
            var newItem = rouletteModel.Roulette[elementIndex];
            newItem.Bets.Add(new BetDTO()
            {
               UserId = request.UserId,
               BetNumber = request.BetNumber,
               BetCash = request.BetCash,
               BetColor = request.BetColor
            }
            );
            rouletteModel.Roulette[elementIndex] = newItem;
            cacheService.SetUsingCache("Roulette", rouletteModel);

            GeneralResponse generalResponse = new GeneralResponse(Constants.APUESTA_EXITOSA);
            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false);
         }
      }

      public GeneralResponse CloseBets(CloseBetsRequest request)
      {
         try
         {
            string Roulette = cacheService.GetUsingCache("Roulette");
            RouletteModel rouletteModel = new RouletteModel();
            if (!String.IsNullOrEmpty(Roulette))
               rouletteModel = JsonConvert.DeserializeObject<RouletteModel>(cacheService.GetUsingCache("Roulette"));
            else
               return new GeneralResponse(false);

            var total = rouletteModel.Roulette.SelectMany(t => t.Bets).Sum(c => c.BetCash);
            var numeroApuestas = rouletteModel.Roulette.Select(t => t.Bets).Count();

            int numero = new Random().Next(0, 1);
            RouletteClosedDTO rouletteClosed = new RouletteClosedDTO();
            rouletteClosed.NumberWinner = numero;
            var ganadores = rouletteModel.Roulette.SelectMany(t => t.Bets)
                                                   .Where(t => t.BetNumber == numero)
                                                   .Select(t => new WinnerUserDto() { UserId = t.UserId, 
                                                                                       Value = String.IsNullOrEmpty(t.BetColor) ? t.BetCash * 5 : t.BetCash * 1.8});

            rouletteClosed.WinnerUsers = ganadores;
            GeneralResponse generalResponse = new GeneralResponse(Constants.CERRADO_RULETA_EXITOSA, ganadores);
            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false);
         }
      }

      public GeneralResponse GetRouletteList()
      {
         try
         {
            string rouletteCache = cacheService.GetUsingCache("Roulette");
            RouletteModel response = JsonConvert.DeserializeObject<RouletteModel>(rouletteCache);
            GeneralResponse generalResponse = new GeneralResponse(Constants.LISTA_RULESTAS_EXITOSO, response);
            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false);
         }
      }
   }
}
