
namespace DiplomStore.ViewsModels.Tovar
{
    public class IndexViewModel
    {
        public IEnumerable<TovarViewModel> Tovar { get; }
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<TovarViewModel> _Tovar, PageViewModel _PageViewModel)
        {
            Tovar = _Tovar;
            PageViewModel = _PageViewModel;
        }
    }
}
