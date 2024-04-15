namespace AspLab1.Models
{
    public interface IDemoModel
    {
        double PositiveNumber { get; set; }
        double NegativeNumber { get; set; }
        string ShortText { get; set; }
        string LongText { get; set; }
    }
}
