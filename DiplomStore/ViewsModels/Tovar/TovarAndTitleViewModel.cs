using DiplomStore.ViewsModels.Title;

namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarAndTitleViewModel
    {
        public List<TovarViewModel>tovars { get; set; }
        public IndexViewModel IndexViewModel { get; set; }
        public TitleViewModel title { get; set; }
    }
}
