
using System.Collections.Generic;

namespace CleanCodeTest.Model.Dto
{
   public class RouletteClosedDTO
   {
      public int NumberWinner { get; set; }
      public IEnumerable<WinnerUserDto> WinnerUsers { get; set; }
   }
}
