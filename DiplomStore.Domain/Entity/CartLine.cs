

namespace DiplomStore.Domain.Entity
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Tovars tovar { get; set; }
        public int Quantity { get; set; }
    }
}
