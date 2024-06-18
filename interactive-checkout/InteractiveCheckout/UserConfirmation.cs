namespace InteractiveCheckout;

public class UserConfirmation
{
    private readonly bool _accepted;

    public UserConfirmation(string message)
    {
        
        Notify($"{message} Choose Option (Y yes) (N no):");
        var result = InputReader();
        _accepted = result != null && result.ToLower() == "y";
    }

    protected virtual string InputReader()
    {
        return Console.ReadLine();
    }

    protected virtual void Notify(string message)
    {
        Console.WriteLine(message);
    }

    public bool WasAccepted()
    {
        return _accepted;
    }
}