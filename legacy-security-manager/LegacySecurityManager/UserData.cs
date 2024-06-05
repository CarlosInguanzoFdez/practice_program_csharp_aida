using System;

namespace LegacySecurityManager;

public record UserData(string Username, string FullName, string Password, string ConfirmPassword)
{
    public const int MinimumPasswordSize = 8;

    public bool PasswordNoMatch()
    {
        return Password != ConfirmPassword;
    }

    public bool PasswordIsTooShort()
    {
        return Password.Length < MinimumPasswordSize;
    }

    public char[] GetEncryptedPassword()
    {
        // (just reverse it, should be secure)
        char[] array = Password.ToCharArray();
        Array.Reverse(array);
        return array;
    }
}