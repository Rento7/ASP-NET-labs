using AspLab1.Extensions;
using AspLab1.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AspLab1.Services
{
    public class ValidationService : IValidationService
    {
        ICarService _carService;


        public ValidationService(IServiceProvider serviceProvider) 
        {
            _carService = serviceProvider.GetService<ICarService>();
        }

        bool ValidtaeModelSpecific(object model, out string[] errors)
        {
            errors = null;
            return model switch
            {
                ICarViewModel => _carService.Validate(((ICarViewModel)model).Car, out errors),
                _ => true
            };
        }

        public bool Validate(object model, out string[] errors)
        {
            List<string> _errors = new List<string>();

            foreach(var property in model.GetType().GetProperties()) 
            {
                foreach(var attribute  in property.GetCustomAttributes()) 
                {
                    var validation = attribute as ValidationAttribute;
                    if (validation is null)
                        continue;

                    if (!validation.IsValid(property.GetValue(model))) 
                        _errors.Add(validation.ErrorMessage != null 
                            ? validation.ErrorMessage 
                            : $"{validation.GetType} not valid on {property.Name} of {model.GetType()}");
                }
            }
            
            if(!ValidtaeModelSpecific(model, out var modelSpecificValidations)) 
            {
                _errors.AddRange(modelSpecificValidations);
            }

            errors = _errors.ToArray();
            return errors.Length == 0;
        }
    }
}
