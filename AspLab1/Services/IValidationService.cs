namespace AspLab1.Services
{
    public interface IValidationService
    {
        bool Validate(object model, out string[] errors);
    }
}
