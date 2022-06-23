namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarsTitleViewModel
    {
        public TovarViewModel Tovar { get; set; }
        public IndexViewModel IndexViewModel { get; set; }
        public IEnumerable<TovarViewModel> Tovars { get; set; }   
        
    }
}
