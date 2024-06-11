



namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommmand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommmandValidator : AbstractValidator<CreateProductCommmand>
    {
        public CreateProductCommmandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Categor is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommmand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommmand command, CancellationToken cancellationToken)
        {

            //create product from command
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //save to database
            session.Store(product);
            await session.SaveChangesAsync();
            //return result
            return new CreateProductResult(product.Id);
        }
    }
}
