using AspLab1.Models;
using AspLab1.ViewModels;

namespace AspLab1.Services
{
    public interface IDemoService
    {
        public void AddItem(IDemoModel item);
        public IEnumerable<IDemoModel> GetItems();
    }
}
