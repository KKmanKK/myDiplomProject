namespace DiplomStore.BLL.DTO.Cart
{
    public class CartDTO
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(TovarDTO tovar, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(p=>p.tovar.TovarsId ==tovar.TovarsId);
            if(line == null)
            {
                lineCollection.Add(new CartLine
                {
                    tovar = tovar,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(TovarDTO tovar) =>lineCollection.RemoveAll(l=>l.tovar.TovarsId == tovar.TovarsId);
        public virtual void RemoveItem(TovarDTO tovar, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(p=>p.tovar.TitleId == tovar.TitleId);
            if(line != null)
            {                
                line.Quantity -= quantity;
            }
        }
        public virtual decimal ComputeTotalVaule() => lineCollection.Sum(e => e.tovar.price * e.Quantity);
        public virtual void Clear()=>lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public TovarDTO tovar { get; set; }
        public int Quantity { get; set; }
    }
}
