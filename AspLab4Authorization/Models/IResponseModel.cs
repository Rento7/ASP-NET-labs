namespace AspLab4Authorization.Models
{
    public interface IResponseModel
    {
        bool Success { get; set; }
        string? Message { get; set; }
        int StatusCode { get; set; }
        int? Id {get;set;}
    }
}
