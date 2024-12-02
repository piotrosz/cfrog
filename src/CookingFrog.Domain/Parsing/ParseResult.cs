namespace CookingFrog.Domain.Parsing;

public record ParseResult<TResult>
{
    private ParseResult(TResult result)
    {
        Result = result;
    }

    private ParseResult(string error, string part)
    {
        ErrorDescription = error;
        Result = default!;
        Part = part;
    }
    
    public string? ErrorDescription { get; }
    
    public string? Part { get; }

    public TResult Result { get; set; }
    
    public bool IsSuccess => ErrorDescription == null;

    public static ParseResult<TResult> Error(string error, string? part = null)
    {
        return new ParseResult<TResult>(error, part);
    }
    
    public static ParseResult<TResult> Success(TResult result)
    {
        return new ParseResult<TResult>(result);
    }
}