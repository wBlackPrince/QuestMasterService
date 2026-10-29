using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record MatchRight
{
    public string Value { get; private set; } = string.Empty;

    private MatchRight() { Value = string.Empty; }

    public static Result<MatchRight, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<MatchRight, AppError>(new AppError("Match right side cannot be empty."));
        if (value.Length > 500)
            return Result.Failure<MatchRight, AppError>(new AppError("Match right side cannot exceed 500 characters."));

        return Result.Success<MatchRight, AppError>(new MatchRight { Value = value });
    }
}