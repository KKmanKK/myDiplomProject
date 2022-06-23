using DiplomStore.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DiplomStore.ViewsModels.Title
{
    public class TitleViewModel
    {
        public int TitlesId { get; set; }
        [Required]
        [Display(Name = "Название тайтла")]
        public string name { get; set; }
        [Display(Name = "Название изображения")]
        public string? NameImg { get; set; }

        [Display(Name = "Популярный тайтл?")]
        public bool isPopular { get; set; }

        public List<Tovars> tovars { get; set; } = new List<Tovars>();
    }
}
