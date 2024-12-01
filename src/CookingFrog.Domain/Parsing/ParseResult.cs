namespace CookingFrog.Domain.Parsing;

public record ParseResult<TResult>
{
    private ParseResult(TResult result)
    {
        Result = result;
    }

    private ParseResult(string error)
    {
        ErrorDescription = error;
        Result = default!;
    }
    
    public string? ErrorDescription { get; }
    
    public TResult Result { get; set; }
    
    public bool IsSuccess => ErrorDescription == null;

    public static ParseResult<TResult> Error(string error)
    {
        return new ParseResult<TResult>(error);
    }
    
    public static ParseResult<TResult> Success(TResult result)
    {
        return new ParseResult<TResult>(result);
    }
}