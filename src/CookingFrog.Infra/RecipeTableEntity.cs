using Azure;
using Azure.Data.Tables;

namespace CookingFrog.Infra;

public sealed record RecipeTableEntity : ITableEntity
{
    public required string PartitionKey { get; set; }
    
    public required string RowKey { get; set; }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
    
    public required string SerializedIngredients { get; set; }

    public required string SerializedSteps { get; set; }

    public required string Summary { get; set; }

    public string? Notes { get; set; }
    
    public TimeSpan TimeToPrepare { get; init; }
}