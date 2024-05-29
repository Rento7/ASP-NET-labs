namespace AspLab4Authorization.Models
{
    public interface IResponseDataModel<T> : IResponseModel
    {
        public T Data { get; set; }
    }
}
