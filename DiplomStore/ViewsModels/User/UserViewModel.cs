using DiplomStore.ViewsModels.Order;

namespace DiplomStore.ViewsModels.User
{
    public class UserViewModel
    {
        public IEnumerable<OrderViewModel> orders { get; set; }
    }
}
