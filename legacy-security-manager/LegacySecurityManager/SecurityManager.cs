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
        Print("Enter a username");
        var username = ReadMessage();
        Print("Enter your full name");
        var fullName = ReadMessage();
        Print("Enter your password");
        var password = ReadMessage();
        Print("Re-enter your password");
        var confirmPassword = ReadMessage();

        if (password != confirmPassword)
        {
            Print("The passwords don't match");
            return;
        }

        if (password.Length < 8)
        {
            Print("Password must be at least 8 characters in length");
            return;
        }

        // Encrypt the password (just reverse it, should be secure)
        char[] array = password.ToCharArray();
        Array.Reverse(array);

        Print($"Saving Details for User ({username}, {fullName}, {new string(array)})\n");
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