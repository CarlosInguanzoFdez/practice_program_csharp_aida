namespace LegacySecurityManager;

public class SecurityManager
{
    private Notifier _consoleNotifier;
    private readonly Reader _consoleReader;

    public SecurityManager(Notifier consoleNotifier, Reader consoleReader)
    {
        _consoleNotifier = consoleNotifier;
        _consoleReader = consoleReader;
    }

    public static void CreateUser()
    {
        new SecurityManager(new ConsoleNotifier(), new ConsoleReader()).CreateUserInstance();
    }

    public void CreateUserInstance()
    {
        var user = RequestUserData();

        if (user.PasswordNoMatch())
        {
            NotifyPasswordDontMatch();
            return;
        }

        if (user.PasswordIsTooShort())
        {
            NotifyPasswordIsTooShort();
            return;
        }

        NotifySavingUser(user);
    }

    private void NotifySavingUser(UserData user)
    {
        Print($"Saving Details for User ({user.Username}, {user.FullName}, {new string(user.GetEncryptedPassword())})\n");
    }

    private void NotifyPasswordIsTooShort()
    {
        Print("Password must be at least 8 characters in length");
    }

    private void NotifyPasswordDontMatch()
    {
        Print("The passwords don't match");
    }

    private UserData RequestUserData()
    {
        Print("Enter a username");
        var username = ReadMessage();
        Print("Enter your full name");
        var fullName = ReadMessage();
        Print("Enter your password");
        var password = ReadMessage();
        Print("Re-enter your password");
        var confirmPassword = ReadMessage();
        return new UserData(username, fullName, password, confirmPassword);
    }

    protected virtual string ReadMessage()
    {
        return _consoleReader.Read();
    }

    protected virtual void Print(string message)
    {
        _consoleNotifier.Notify(message);
    }
}