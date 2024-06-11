

namespace Catalog.API.Products.GetProducts
{
    public record GetProducsQuery( )
        : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler (IDocumentSession session) 
        : IQueryHandler<GetProducsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProducsQuery query, CancellationToken cancellationToken)
        {
            var products = await  session.Query<Product>()
                .ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
