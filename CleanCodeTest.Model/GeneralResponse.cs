namespace CleanCodeTest.Model
{
   public class GeneralResponse
   {
      public GeneralResponse(bool success, string message)
      {
         Success = success;
         Message = message;
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
