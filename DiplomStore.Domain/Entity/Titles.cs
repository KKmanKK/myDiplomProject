namespace DiplomStore.Domain.Entity
{
    public class Titles
    {
        public int TitlesId { get; set; }

        public string name { get; set; }
        
        public bool? isPopular { get; set; }
        public string? imgPath { get; set; }

        public List<Tovars> tovars { get; set; }=new List<Tovars>();
    }
}
