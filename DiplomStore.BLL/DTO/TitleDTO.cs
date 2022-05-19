using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.DTO
{
    public class TitleDTO
    {
        public int TitlesId { get; set; }

        public string name { get; set; }
        public bool? isPopular { get; set; }

        public List<Tovars> tovars { get; set; } = new List<Tovars>();
    }
}
