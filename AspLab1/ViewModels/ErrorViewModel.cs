using AspLab1.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AspLab1.ViewModels
{
    public class ErrorViewModel
    {
        int _statusCode;
        
        public ErrorViewModel(int statusCode)
        {
            _statusCode = statusCode;
        }

        public int StatusCode => _statusCode;
    }
}
