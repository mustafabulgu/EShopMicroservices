namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItemModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }
    public class ShoppingCartItemModel
    {
        public Guid ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }

    //wrapper
    public record GetBasketResponse(ShoppingCartModel Cart);
    public record StoreBasketRequest(ShoppingCartModel Cart);

    public record StoreBasketReqsponse(string UserName);

    public record DeleteBasketResponse(bool IsSucess);
}
