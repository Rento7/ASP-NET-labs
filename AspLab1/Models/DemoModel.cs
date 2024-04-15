using System.ComponentModel.DataAnnotations;

namespace AspLab1.Models
{
    public class DemoModel : IDemoModel
    {
        public double PositiveNumber { get; set; }

        public double NegativeNumber { get; set; }

        public string ShortText { get; set; }

        public string LongText { get; set; }
    }
}
