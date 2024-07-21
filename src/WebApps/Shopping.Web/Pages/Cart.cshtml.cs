
namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCart(Guid productId)
        {
            logger.LogInformation("Remove from cart  {productId}", productId);
            Cart = await basketService.LoadUserBasket();
            Cart.Items.RemoveAll(item => item.ProductId == productId);
            await basketService.StoreBasket(new StoreBasketRequest(Cart));
            return RedirectToPage();
        }
    }
}
