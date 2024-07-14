
using Basket.API.Basket.DeleteBasket;
using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<BasketCheckoutResult>;
    public record BasketCheckoutResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto is required");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, BasketCheckoutResult>
    {
        public async Task<BasketCheckoutResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //get existing basket with price
            //set totalprice on event
            //send basket checkout event to subscribers - rabbit
            //delete basket
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if(basket == null)
            {
                return new BasketCheckoutResult(false);
            }

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);
            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new BasketCheckoutResult(true);
        }
    }
}
