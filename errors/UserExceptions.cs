namespace UserStorageConsole.errors;

public class NoUsersException : Exception
{
    private const string message = "Users have not been created yet:(\n";
    public NoUsersException() : base(message){}
}

public class InvalidPasswordException : Exception
{
    private const string message = "Invalid Password\n";
    public InvalidPasswordException() : base(message){}
}

public class UserDoesNotExistException : Exception
{
    private const string message = "User with this name does not exist\n";
    public UserDoesNotExistException() : base(message){}
}

public class UserAlreadyExistException : Exception
{
    private const string message = "User with this name already exist\n";
    public UserAlreadyExistException() : base(message){}
}