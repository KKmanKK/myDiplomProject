using DiplomStore.ViewsModels.Category;
using DiplomStore.ViewsModels.Title;

namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarAndCategoryListViewModel
    {
        public List<TovarViewModel> Tovar { get; set; }
        public IEnumerable<TitleViewModel> Titles { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<int> selectTitle { get; set; } = new List<int>();
        
    }
}
