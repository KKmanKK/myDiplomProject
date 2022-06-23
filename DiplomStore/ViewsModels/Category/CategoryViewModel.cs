using DiplomStore.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DiplomStore.ViewsModels.Category
{
    public class CategoryViewModel
    {
        public int CategoriesId { get; set; }
        [Required]
        [Display(Name = "Название категории")]
        public string name { get; set; }
        [Display(Name = "Популярная категория?")]
        public bool IsPopular { get; set; }
        [Display(Name = "Название изображения")]
        public string? NameImg { get; set; }
        public List<Tovars> tovars { get; set; } = new List<Tovars>();
    }
}
