namespace Mountebank.Exceptions;

public class RecordNotFound : Exception
{
    public RecordNotFound(string message) : base(message) { }
}