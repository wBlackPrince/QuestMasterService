using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record MatchLeft
{
    public string Value { get; private set; } = string.Empty;

    private MatchLeft() { Value = string.Empty; }

    public static Result<MatchLeft, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<MatchLeft, AppError>(new AppError("Match left side cannot be empty."));
        if (value.Length > 500)
            return Result.Failure<MatchLeft, AppError>(new AppError("Match left side cannot exceed 500 characters."));

        return Result.Success<MatchLeft, AppError>(new MatchLeft { Value = value });
    }
}