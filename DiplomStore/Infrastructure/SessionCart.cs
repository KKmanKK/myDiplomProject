using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.DTO;
using Newtonsoft.Json;

namespace DiplomStore.Infrastructure
{
    public class SessionCart:CartDTO
    {
        public static CartDTO GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(TovarDTO tovar, int quantity)
        {
            base.AddItem(tovar, quantity);
            Session.SetJson("Cart",this);
        }
        public override void RemoveItem(TovarDTO tovar, int quantity)
        {
            base.RemoveItem(tovar, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(TovarDTO tovar)
        {
            base.RemoveLine(tovar);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
