
namespace DiplomStore.Domain.Entity
{
    public class Order
    {
        public int OrderID { get; set;}
        public ICollection<CartLine> Lines { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public int? House { get; set; }

        public DateTime DateTime { get; set; }
        public AppUser User { get; set; }

    }
}
