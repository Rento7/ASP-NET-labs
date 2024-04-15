using AspLab1.Models;

namespace AspLab1.Services
{
    public interface ICarService
    {
        public void AddCar(ICarModel car);
        public IEnumerable<ICarModel>  GetCars();
        public bool Validate(ICarModel model, out string[] errors);
    }
}
