

namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger) 
        : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];
        [BindProperty(SupportsGet =true)]
        public string SelectedCategory { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string categoryId)
        {
            var response = await catalogService.GetProducts();
            CategoryList = response.Products.SelectMany(p => p.Category).Distinct().ToList();
            
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                ProductList = response.Products.Where(p => p.Category.Contains(categoryId));
                SelectedCategory = categoryId;
            }
            else
            {
                ProductList = response.Products;
            }

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
