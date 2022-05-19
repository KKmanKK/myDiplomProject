using DiplomStore.Domain.Entity;

namespace DiplomStore.ViewsModels.Category
{
    public class CategoryViewModel
    {
        public int CategoriesId { get; set; }
        public string name { get; set; }
        public bool IsPopular { get; set; }
        public string? imgPath { get; set; }
        public List<Tovars> tovars { get; set; } = new List<Tovars>();
    }
}
