namespace Entities.RequestParameters;

public class ProductRequestParameters : RequestParameters
{
    public int? CategoryId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public ProductRequestParameters() : this(1, 6)
    {
        
    }
    
    public ProductRequestParameters(int page=1, int pageSize=6) : base()
    {
        Page = page;
        PageSize = pageSize;
    }
    
}