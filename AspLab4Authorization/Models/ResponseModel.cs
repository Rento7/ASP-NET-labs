namespace AspLab4Authorization.Models
{
    public class ResponseModel : IResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public int? Id { get; set; }
    }
}
