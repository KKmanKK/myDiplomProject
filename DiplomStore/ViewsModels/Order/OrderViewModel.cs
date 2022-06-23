using DiplomStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DiplomStore.ViewsModels.Order
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine>? Lines { get; set; }
        [Required(ErrorMessage = "Введите населенный пункт")]
        public string Locality { get; set; }
        [Required(ErrorMessage ="Введите регион")]
        public string Region { get; set; }
        [Required(ErrorMessage ="Введите улицу")]
        public string Street { get; set; }
        
        [Range(minimum:1,maximum:10000, ErrorMessage ="Введите номер дома")]
        public int House { get; set; }

        public DateTime DateTime { get; set; }

        [BindNever]
        public AppUser? User { get; set; }
    }
}
