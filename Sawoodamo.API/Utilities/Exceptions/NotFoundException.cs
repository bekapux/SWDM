namespace Sawoodamo.API.Utilities.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string entityName) : base($"{entityName} with provided ID was not found")
    {

    }
    public NotFoundException(string entityName, int id) : base($"{entityName} with ID '{id}' was not found")
    {

    }

    public NotFoundException(string entityName, string id) : base($"{entityName} with ID '{id}' was not found")
    {

    }
}
