using CookingFrog.Domain;

namespace CookingFrog.Infra;

public static class RecipeSummaryToRecipeSummaryTableEntityMapper
{
    public static RecipeSummaryTableEntity MapToTableEntity(this RecipeSummary recipeSummary)
    {
        return new RecipeSummaryTableEntity
        {
            PartitionKey = "_default",
            RowKey = recipeSummary.Guid.ToString(),
            Summary = recipeSummary.Summary
        };
    }
}