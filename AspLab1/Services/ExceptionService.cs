namespace AspLab1.Services
{
    public class ExceptionService : IExceptionService
    {
        List<Exception> _exceptions = new List<Exception>();

        public void Add(Exception exception) => _exceptions.Add(exception);

        public Exception LastException => _exceptions.LastOrDefault();
    }
}
