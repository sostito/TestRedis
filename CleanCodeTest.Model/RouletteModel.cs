
using CleanCodeTest.Model.Dto;
using System.Collections.Generic;

namespace CleanCodeTest.Model
{
   public class RouletteModel
   {
      public RouletteModel()
      {
         Roulette = new List<RouletteDTO>();
      }
      public List<RouletteDTO> Roulette { get; set; }
   }
}
