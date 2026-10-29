using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record OptionText
{
    public string Value { get; private set; } = string.Empty;

    private OptionText() { Value = string.Empty; }

    public static Result<OptionText, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<OptionText, AppError>(new AppError("Option text cannot be empty."));
        if (value.Length > 500)
            return Result.Failure<OptionText, AppError>(new AppError("Option text cannot exceed 500 characters."));

        return Result.Success<OptionText, AppError>(new OptionText { Value = value });
    }
}