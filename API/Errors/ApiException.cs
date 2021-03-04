namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null, string error =null)
        {
            StatusCode = statusCode;
            Message = message;
            Error = error;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}