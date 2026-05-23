namespace Cms.Shared.Domain.Errors;

public class Error
{
    public string Code { get; }
    public string Message { get; }

    public static readonly Error None = new(string.Empty, string.Empty);

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString() => $"{Code}: {Message}";
}
