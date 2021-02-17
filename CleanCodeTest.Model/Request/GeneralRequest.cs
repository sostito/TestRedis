using System.ComponentModel.DataAnnotations;

namespace CleanCodeTest.Model
{
   public class GeneralRequest
   {
      [Required(ErrorMessage = "El campo es requerido")]
      public int RouletteId { get; set; }
   }
}
