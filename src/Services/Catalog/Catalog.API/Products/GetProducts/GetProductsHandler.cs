

namespace Catalog.API.Products.GetProducts
{
    public record GetProducsQuery( )
        : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler (IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
        : IQueryHandler<GetProducsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProducsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
            var products = await  session.Query<Product>()
                .ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
