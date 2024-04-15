using AspLab1.Models;
using System.ComponentModel.DataAnnotations;

namespace AspLab1.ViewModels
{
    public class CarViewModel : ICarViewModel
    {
        ICarModel _car;

        public CarViewModel(ICarModel car) 
        {
            _car = car;
        }

        public CarViewModel() : this(new CarModel() { Owner = new CarOwnerModel() } )
        {
        }

        public ICarModel Car => _car;

        [Required(ErrorMessage = "Owner name cannot be empty")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Owner name length must no more than 100 characters and no less 2 characters")]
        public string OwnerFullName { get => _car.Owner.FullName; set => _car.Owner.FullName = value; }

        [Required(ErrorMessage = "Car name cannot be empty")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Car Name length must no more than 100 characters and no less 2 characters")]
        public string Name { get => _car.Name; set => _car.Name = value; }

        [RegularExpression(@"^[A-Z]{2}\d{4}[A-Z]{2}$", ErrorMessage = "Wrong registration plate")]
        public string RegistrationPlate { get => _car.RegistrationPlate; set => _car.RegistrationPlate = value; }

        [Required(ErrorMessage = "Engine volume cannot be empty")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Engine volume must be positive")]
        public double EngineVolume { get => _car.EngineVolume; set => _car.EngineVolume = value; }

        [Required(ErrorMessage = "Mileage cannot be empty")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Mileage must be positive")]
        public double Mileage { get => _car.Mileage; set => _car.Mileage = value; }

        public Drivetrain Drivetrain { get => _car.Drivetrain; set => _car.Drivetrain = value; }
    }
}
