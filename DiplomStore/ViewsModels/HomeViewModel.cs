using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;
using DiplomStore.ViewsModels.Tovar;

namespace DiplomStore.ViewsModels
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModel> categories { get; set; }
        public IEnumerable<ListTovarViewModel> tovars { get; set; }
        public IEnumerable<TitleViewModel> titles { get; set; }
    }
}
