using System.ComponentModel.DataAnnotations;

namespace CleanCodeTest.Model.Responses
{
   public class MakeBetRequest : GeneralRequest
   {
      public string BetColor { get; set; }
      [Range(0, 36, ErrorMessage = "El número debe estar entre 0 y 36")]
      public int BetNumber { get; set; }
      [Required(ErrorMessage = "El campo es requerido")]
      public int BetCash { get; set; }
      public string UserId { get; set; }
   }
}
