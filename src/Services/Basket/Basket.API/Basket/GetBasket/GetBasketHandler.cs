


using BuildingBlocks.Messaging.Events;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler (IBasketRepository repository): IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basketCheckoutEvent = new BasketCheckoutEvent();
            var id = basketCheckoutEvent.Id;
            var id2 = basketCheckoutEvent.Id;

            var basket =  await repository.GetBasket(query.UserName, cancellationToken);
            return new GetBasketResult(basket);
        }
    }
}
