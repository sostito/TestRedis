namespace CleanCodeTest.Model
{
   public class GeneralResponse
   {
      public GeneralResponse(bool success)
      {
         Success = success;
      }

      public GeneralResponse(string message, object data = null)
      {
         Success = true;
         Data = data;
         Message = message;
      }

      public object Data { get; set; }
      public bool Success { get; set; }
      public string Message { get; set; }
   }
}
