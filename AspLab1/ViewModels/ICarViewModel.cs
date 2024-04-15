using AspLab1.Models;

namespace AspLab1.ViewModels
{
    public interface ICarViewModel
    {
        string OwnerFullName { get; set; }
        string Name { get; set; }
        string RegistrationPlate { get; set; }
        double EngineVolume { get; set; }
        double Mileage { get; set; }
        Drivetrain Drivetrain { get; set; }

        ICarModel Car { get; }
    }
}
