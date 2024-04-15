namespace AspLab1.Models
{
    public interface ICarModel
    {
        string RegistrationPlate { get; set; }
        string Name { get; set; }
        double EngineVolume { get; set; }
        double Mileage { get; set; }
        Drivetrain Drivetrain { get; set; }
        ICarOwnerModel Owner { get; set; }
    }
}
