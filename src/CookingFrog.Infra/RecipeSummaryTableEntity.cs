using Azure;
using Azure.Data.Tables;

namespace CookingFrog.Infra;

public record RecipeSummaryTableEntity : ITableEntity
{
    public string? PartitionKey { get; set; }
    
    public required string RowKey { get; set; }
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }

    public required string Summary { get; set; }
}