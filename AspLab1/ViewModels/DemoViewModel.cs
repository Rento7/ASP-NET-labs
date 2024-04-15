using AspLab1.Models;
using System.ComponentModel.DataAnnotations;

namespace AspLab1.ViewModels
{
    public class DemoViewModel : IDemoViewModel
    {
        IDemoModel _model = new DemoModel();
        
        public IDemoModel Model { get => _model; set => _model = value; }

        [Required( ErrorMessage = "Number cannot be empty" )]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Number must be positive")]
        public double PositiveNumber { get => _model.PositiveNumber; set => _model.PositiveNumber = value; }

        [Required(ErrorMessage = "Number cannot be empty")]
        [Range(double.NegativeInfinity, 0, ErrorMessage = "Number must be negative")]
        public double NegativeNumber { get => _model.NegativeNumber; set => _model.NegativeNumber = value; }

        [Required(ErrorMessage = "Text cannot be empty")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Text length must be shorter than 10 characters")]
        public string ShortText { get => _model.ShortText; set => _model.ShortText = value; }

        [Required]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Text length must be longer than 10 characters")]
        public string LongText { get => _model.LongText; set => _model.LongText = value; }
    }
}
