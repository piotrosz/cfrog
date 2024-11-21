using Azure;
using Azure.Data.Tables;

namespace CookingFrog.Infra;

public class RecipeTableEntity : ITableEntity
{
    public required string PartitionKey { get; set; }
    
    public required string RowKey { get; set; }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
    
    public required string SerializedIngredients { get; init; }

    public required string SerializedSteps { get; init; }

    public required string Summary { get; init; }
    
    public TimeSpan TimeToPrepare { get; init; }
}