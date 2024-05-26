namespace AspLab3MinimalApiEntityFramework.Models
{
    public interface IResponseDataModel<T> : IResponseModel
    {
        public T Data { get; set; }
    }
}
