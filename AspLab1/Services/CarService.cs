using AspLab1.Models;

namespace AspLab1.Services
{
    public class CarService : ICarService
    {
        IList<ICarModel> _cars;

        public CarService() 
        {
            OnInitialize();
        }

        void OnInitialize()
        {
            _cars = new List<ICarModel>()
            {
                new CarModel() {RegistrationPlate = "AE1111AE", Name = "Toyta Camry", EngineVolume = 3.0, Mileage= 30000, Drivetrain = Drivetrain.RWD, Owner = new CarOwnerModel() { FullName = "Denis Petrov" } },
                new CarModel() {RegistrationPlate = "AE6666AE", Name = "Maserati Ghibli Modena", EngineVolume = 3.0, Mileage= 5000, Drivetrain = Drivetrain.AWD, Owner = new CarOwnerModel() { FullName = "Robert Noname" } },
                new CarModel() {RegistrationPlate = "AE7777AE", Name = "Porsche 911", EngineVolume = 3.6, Mileage= 64000, Drivetrain = Drivetrain.AWD, Owner = new CarOwnerModel() { FullName = "Valerii Imennyi" } },
            };
        }

        public void AddCar(ICarModel car)
        {
            _cars.Add(car);
        }

        public IEnumerable<ICarModel> GetCars()
        {
            return _cars;
        }

        public bool Validate(ICarModel car, out string[] errors) 
        {
            var _errors = new List<string>();

            if(car is null)
            {
                throw new ArgumentException($"Cannot validate null instance. Type of {typeof(ICarModel)}");
            }

            if(_cars.FirstOrDefault(c => c.RegistrationPlate == car.RegistrationPlate) != null)
            {
                _errors.Add("Car with such registration plate exist");
            }    

            errors = _errors.ToArray();
            return errors.Length == 0;
        }
    }
}
