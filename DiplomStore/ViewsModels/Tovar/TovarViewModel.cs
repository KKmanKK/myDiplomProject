using DiplomStore.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarViewModel
    {
        public int TovarsId { get; set; }
        [Required]
        [Display(Name = "Название товара")]

        public string name { get; set; }       
        [Range(minimum:0, maximum:1000000)]
        [Display(Name = "Цена товара")]
        public decimal price { get; set; }
        [Display(Name = "В налиции?")]
        public bool isValids { get; set; }
        [Display(Name = "Высота")]
        public int? hight { get; set; }
        [Display(Name = "Ширина")]
        public int? width { get; set; }
        [Range(minimum: 0, maximum: 1000000)]
        [Display(Name = "Число")]
        public int count { get; set; }
        [Display(Name = "Название изображения")]
        public string? NameImg { get; set; }
        [Display(Name = "Материал")]
        public string? materials { get; set; }
        [Display(Name = "Наполнение")]
        public string? filler { get; set; }

        public string nameCategory { get; set; }
        public int CategoryId { get; set; }
        public Categories? category { get; set; }

        public string nameTitle { get; set; }

        public int TitleId { get; set; }
        public Titles? titles { get; set; }

    }
}
