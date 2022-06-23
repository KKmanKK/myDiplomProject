using DiplomStore.BLL.DTO.Cart;
using DiplomStore.BLL.DTO;
using Newtonsoft.Json;
using DiplomStore.Domain.Entity;

namespace DiplomStore.Infrastructure
{
    public class SessionCart:CartDTO
    {
        public static CartDTO GetCart(IServiceProvider service)
        {
            //Получаем данные сессии
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Tovars tovar, int quantity)
        {
            base.AddItem(tovar, quantity);
            Session.SetJson("Cart",this);
        }
        public override void RemoveItem(Tovars tovar)
        {
            base.RemoveItem(tovar);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Tovars tovar)
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
