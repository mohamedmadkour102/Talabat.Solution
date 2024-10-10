
namespace Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massage { get; set; }
        public ApiResponse(int statusCode , string? massage =null)
        {
            StatusCode = statusCode;
            Massage = massage ?? GetDefaultMassageForStatusCode(statusCode);
            
        }

        private string? GetDefaultMassageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "BadRequest",
                401 => "UnAuthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
