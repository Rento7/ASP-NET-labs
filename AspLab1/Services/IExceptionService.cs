namespace AspLab1.Services
{
    public interface IExceptionService
    {
        Exception LastException { get; }
        void Add(Exception exception);
    }
}
