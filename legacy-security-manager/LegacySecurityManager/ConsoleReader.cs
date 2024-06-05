using System;

namespace LegacySecurityManager;

public class ConsoleReader : Reader
{
    public string Read()
    {
        return Console.ReadLine();
    }
}