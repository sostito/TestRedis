using System.Collections.Generic;

namespace CleanCodeTest.Model.Dto
{
   public class RouletteDTO
   {
      public RouletteDTO()
      {
         Bets = new List<BetDTO>();
      }
      public int RouletteId { get; set; }
      public bool Enabled { get; set; }
      public List<BetDTO> Bets { get; set; }
   }
}
