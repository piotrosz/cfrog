using Azure;
using Azure.Data.Tables;

namespace CookingFrog.Infra;

public class RecipeSummaryTableEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string Summary { get; set; }
}