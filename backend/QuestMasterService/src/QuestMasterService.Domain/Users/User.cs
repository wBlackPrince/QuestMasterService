using CSharpFunctionalExtensions;

namespace QuestMasterService.Domain;


public sealed class User
{
    public Guid Id { get; }
    public UserName Name { get; private set; }

    private User(Guid id, UserName name)
    {
        Id = id;
        Name = name;
    }

    public static Result<User, AppError> Create(string name)
    {
        Result<UserName, AppError> userNameResult = UserName.Create(name);
        if (userNameResult.IsFailure)
        {
            return Result.Failure<User, AppError>(userNameResult.Error);
        }

        Guid id = Guid.NewGuid();
        User user = new User(id, userNameResult.Value);
        return Result.Success<User, AppError>(user);
    }


    public UnitResult<AppError> ChangeName(string newValue)
    {
        Result<UserName, AppError> newName = UserName.Create(newValue);

        if (newName.IsFailure)
        {
            return UnitResult.Failure<AppError>(newName.Error);
        }

        Name = newName.Value;

        return UnitResult.Success<AppError>();
    }

}
