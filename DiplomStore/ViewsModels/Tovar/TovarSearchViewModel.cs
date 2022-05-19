namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarSearchViewModel
    {
        public IEnumerable<TovarViewModel> Tovars { get; set; }
        public string searchString { get; set; }
    }
}
