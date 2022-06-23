using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.DTO.Cart
{
    public class CartDTO
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Tovars tovar, int quantity)
        {       
            CartLine line = lineCollection.FirstOrDefault(p=>p.tovar.TovarsId == tovar.TovarsId);
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

        public virtual void RemoveLine(Tovars tovar) =>lineCollection.RemoveAll(l=>l.tovar.TovarsId == tovar.TovarsId);
        public virtual void RemoveItem(Tovars tovar)
        {
            CartLine line = lineCollection.FirstOrDefault(p=>p.tovar.TovarsId == tovar.TovarsId);
            if(line != null & line.Quantity >=2)
            {                
                line.Quantity -= 1; 
                
            }
        }
        public virtual decimal ComputeTotalVaule() => lineCollection.Sum(e => e.tovar.price * e.Quantity);
        public virtual void Clear()=>lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    
}
