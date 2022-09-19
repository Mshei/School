using Microsoft.AspNetCore.Mvc;

namespace ParkingCase
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore shoppingCartStore;
        public ShoppingCartController(IShoppingCartStore shoppingCartStore)
        {
            this.shoppingCartStore = shoppingCartStore;
        }
        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId) =>
        this.shoppingCartStore.Get(userId);

        
    }

    public ShoppingCartController(IShoppingCartStore shoppingCartStore)
    {
        this.shoppingCartStore = shoppingCartStore;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.Scan(selector =>
        selector
        .FromAssemblyOf<Startup>()
        .AddClasses()
        .AsImplementedInterfaces());
    }

    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
        void Save(ShoppingCart shoppingCart);
    }

}
