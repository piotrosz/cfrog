using CookingFrog.Domain;

namespace CookingFrog.Infra;

public static class RecipeSummaryTableEntityToRecipeSummaryMapper
{
    public static RecipeSummary Map(this RecipeSummaryTableEntity recipeSummary)
    {
        return new RecipeSummary(
            Guid.Parse(recipeSummary.RowKey),
            recipeSummary.Summary);
    }
}