namespace AspLab1.Models
{
    public class CarModel : ICarModel
    {
        public string RegistrationPlate { get; set; }
        public string Name { get; set; }
        public double EngineVolume { get; set; }
        public double Mileage { get; set; }
        public Drivetrain Drivetrain { get; set; }
        public ICarOwnerModel Owner { get; set; }
    }
}
