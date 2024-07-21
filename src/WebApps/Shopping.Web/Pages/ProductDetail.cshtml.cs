

namespace Shopping.Web.Pages
{
    public class ProductDetailModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductDetailModel> logger) 
        : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public string Colour { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProductById(productId);
            Product = response.Product;
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked");
            var productResponse = await catalogService.GetProductById(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Colour = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
