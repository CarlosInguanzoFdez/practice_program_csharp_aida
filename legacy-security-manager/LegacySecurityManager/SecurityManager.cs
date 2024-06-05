using System;

namespace LegacySecurityManager;

public class SecurityManager
{
    public static void CreateUser()
    {
        new SecurityManager().CreateUserInstance();
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
        return Console.ReadLine();
    }

    protected virtual void Print(string message)
    {
        Console.WriteLine(message);
    }
}