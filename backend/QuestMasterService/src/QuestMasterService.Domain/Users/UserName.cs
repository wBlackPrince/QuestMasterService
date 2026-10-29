using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;

public record UserName
{
    public string Value { get; private set; } = string.Empty;

    private UserName()
    {
        Value = string.Empty;
    }

    public static Result<UserName, AppError> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<UserName, AppError>(new AppError("User name cannot be empty."));
        }

        if (value.Length > 50)
        {
            return Result.Failure<UserName, AppError>(new AppError("User name cannot exceed 50 characters."));
        }

        return Result.Success<UserName, AppError>(new UserName { Value = value });
    }
}