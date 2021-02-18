using CleanCodeTest.Common;
using CleanCodeTest.Model;
using CleanCodeTest.Model.Dto;
using CleanCodeTest.Model.Request;
using CleanCodeTest.Model.Responses;
using CleanCodeTest.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanCodeTest.Service
{
   public class RouletteService : IRouletteService
   {
      readonly ICacheService cacheService;
      public RouletteService(ICacheService cacheService)
      {
         this.cacheService = cacheService;
      }
      public async Task<GeneralResponse> CreateRoulette()
      {
         try
         {
            RouletteModel rouletteModel = await GetRuletteModel() != null ? await GetRuletteModel() : new RouletteModel();
            RouletteDTO rouletteDto = new RouletteDTO();
            rouletteDto.RouletteId = new Random().Next(0, 999999999);
            rouletteModel.Roulette.Add(rouletteDto);
            await cacheService.SetUsingCache("Roulette", rouletteModel);
            GeneralResponse generalResponse = new GeneralResponse(Constants.CREACION_RULETA_EXITOSA, rouletteDto.RouletteId);

            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false, Constants.CREACION_RULETA_FALLIDA);
         }
      }
      public async Task<GeneralResponse> OpeningRoulette(RouletteOpeningRequest request)
      {
         try
         {
            RouletteModel rouletteModel = await GetRuletteModel();
            if (rouletteModel == null)
               return new GeneralResponse(false, Constants.APERTURA_RULETA_FALLIDA);

            rouletteModel = ChangeRuletteEnabled(request.RouletteId, true, rouletteModel);
            await cacheService.SetUsingCache("Roulette", rouletteModel);
            GeneralResponse generalResponse = new GeneralResponse(Constants.APERTURA_RULETA_EXITOSA);

            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false, Constants.APERTURA_RULETA_FALLIDA);
         }
      }
      public async Task<GeneralResponse> MakeBet(MakeBetRequest request)
      {
         try
         {
            RouletteModel rouletteModel = await GetRuletteModel();
            if (rouletteModel == null)
               return new GeneralResponse(false, Constants.CREACION_APUESTA_FALLIDA);

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
            await cacheService.SetUsingCache("Roulette", rouletteModel);

            GeneralResponse generalResponse = new GeneralResponse(Constants.CREACION_APUESTA_EXITOSA);
            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false, Constants.CREACION_APUESTA_FALLIDA);
         }
      }
      public async Task<GeneralResponse> CloseBets(CloseBetsRequest request)
      {
         try
         {
            RouletteModel rouletteModel = await GetRuletteModel();
            if (rouletteModel == null)
               return new GeneralResponse(false, Constants.CIERRE_APUESTA_FALLIDA);

            RouletteClosedDTO rouletteClosed = new RouletteClosedDTO();
            rouletteClosed.NumberWinner = new Random().Next(0, 1);
            rouletteClosed.WinnerUsers = GetWinnerUsers(rouletteClosed.NumberWinner, rouletteModel);
            rouletteModel = ChangeRuletteEnabled(request.RouletteId, false, rouletteModel);
            await cacheService.SetUsingCache("Roulette", rouletteModel);
            GeneralResponse generalResponse = new GeneralResponse(Constants.CERRADO_RULETA_EXITOSA, rouletteClosed.WinnerUsers);

            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false, Constants.CIERRE_APUESTA_FALLIDA);
         }
      }
      public async Task<GeneralResponse> GetRouletteList()
      {
         try
         {
            RouletteModel response = await GetRuletteModel();
            if (response == null)
               throw new Exception();
            GeneralResponse generalResponse = new GeneralResponse(Constants.LISTAR_RULESTAS_EXITOSO, response);
            return generalResponse;
         }
         catch (Exception)
         {
            return new GeneralResponse(false, Constants.LISTAR_RULETAS_FALLIDA);
         }
      }
      private async Task<RouletteModel> GetRuletteModel()
      {
         string ruletteCache = await cacheService.GetUsingCache("Roulette");
         var rouletteModel = !string.IsNullOrEmpty(ruletteCache) ?
                              JsonConvert.DeserializeObject<RouletteModel>(ruletteCache) :
                              null;

         return rouletteModel;
      }
      private IEnumerable<WinnerUserDto> GetWinnerUsers(int numberWinner, RouletteModel roulette)
      {
         var winnerUsers = roulette.Roulette.SelectMany(t => t.Bets)
                                                .Where(t => t.BetNumber == numberWinner)
                                                .Select(t => new WinnerUserDto()
                                                {
                                                   UserId = t.UserId,
                                                   Value = String.IsNullOrEmpty(t.BetColor) ? t.BetCash * 5 : t.BetCash * 1.8
                                                });

         return winnerUsers;
      }
      private RouletteModel ChangeRuletteEnabled(int rouletteId, bool enabled, RouletteModel roulette)
      {
         var elementIndex = roulette.Roulette.FindIndex(t => t.RouletteId == rouletteId);
         var newItem = roulette.Roulette[elementIndex];
         newItem.Enabled = enabled;
         roulette.Roulette[elementIndex] = newItem;

         return roulette;
      }
   }
}
