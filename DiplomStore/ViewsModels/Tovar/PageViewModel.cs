namespace DiplomStore.ViewsModels.Tovar
{
    public class PageViewModel
    {
        public int PageNuber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNuber > 1;
        public bool HasNextPage => PageNuber < TotalPages;

        public PageViewModel(int count, int pageNuber, int pageSize)
        {
            PageNuber = pageNuber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
