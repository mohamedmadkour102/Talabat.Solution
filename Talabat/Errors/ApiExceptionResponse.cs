namespace Talabat.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details {  get; set; } 
        public ApiExceptionResponse(int statuscode , string? massage = null , string? details = null) : base (statuscode , massage)
        {
            Details = details;
        }
    }
}
