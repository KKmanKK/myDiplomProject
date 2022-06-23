
namespace DiplomStore.Domain.Entity
{
    public class Tovars
    {
        public int TovarsId { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }

        public bool isValids { get; set; }

        public int? hight { get; set; }

        public int? width { get; set; }
        public int count { get; set; }

        public string? NameImg { get; set; }

        public string? materials { get; set; }

        public string? filler { get; set; }

        public int CategoryId { get; set; }
        public Categories category { get; set; }

        public int TitleId { get; set; }
        public Titles titles { get; set; }
    }
}
