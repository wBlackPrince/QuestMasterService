namespace QuestMasterService.Domain;

public class AppError
{
    public string Message { get; private set; }

    public AppError(string message)
    {
        Message = message;
    }
}

