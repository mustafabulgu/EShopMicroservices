

using System.Net;

namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
    {
      
        public IEnumerable<ProductModel> ProductList{ get; set; } = new List<ProductModel>();
       

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited.");
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Adding cart {productId}", productId);
            var productResponse =  await catalogService.GetProductById(productId);
            var basket = await basketService.LoadUserBasket();
            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productResponse.Product.Id,
                ProductName = productResponse.Product.Name,
                Colour = "Black",
                Price = productResponse.Product.Price,
                Quantity = 1

            });
            await basketService.StoreBasket(new StoreBasketRequest( basket));
            return RedirectToPage("Cart");
        }
      
    }
}
