namespace AspLab1.ViewModels
{
    public class ExceptionViewModel
    {
        Exception _exception;
        
        public ExceptionViewModel(Exception exception) 
        {
            _exception = exception;
        }


        public string Message => _exception.Message;
        public string Type => _exception.GetType().ToString();
    }
}
