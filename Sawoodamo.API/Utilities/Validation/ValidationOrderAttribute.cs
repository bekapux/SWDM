namespace Sawoodamo.API.Utilities.Validation;

[AttributeUsage(AttributeTargets.Class)]
public class ValidationOrderAttribute(int order) : Attribute
{
    public int Order { get; init; } = order;
}
