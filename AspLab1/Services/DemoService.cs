using AspLab1.Models;
using AspLab1.ViewModels;

namespace AspLab1.Services
{
    public class DemoService : IDemoService
    {
        List<IDemoModel> _items;

        public DemoService() 
        {
            OnInitialize();
        }

        void OnInitialize() 
        {
            _items = new List<IDemoModel>()
            {
                new DemoModel() {PositiveNumber = 1, NegativeNumber = -1, ShortText = "Item 1", LongText= "Long long text. Item 1"},
                new DemoModel() {PositiveNumber = 2, NegativeNumber = -2, ShortText = "Item 2", LongText= "Long long text. Item 2"},
                new DemoModel() {PositiveNumber = 3, NegativeNumber = -3, ShortText = "Item 3", LongText= "Long long text. Item 3"},
            };
        }

        public void AddItem(IDemoModel item)
        {
            _items.Add(item);
        }

        public IEnumerable<IDemoModel> GetItems()
        {
            return _items;
        }
    }
}
