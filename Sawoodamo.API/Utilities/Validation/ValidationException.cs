using FluentValidation.Results;

namespace Sawoodamo.API.Utilities.Validation;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures has occurred.")
    {
        Errors = failures
            .Distinct()
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();
    }

    public IReadOnlyCollection<Error> Errors { get; }
}

public sealed class Error : ValueObject
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    internal static Error None => new(string.Empty, string.Empty);
}

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b) => !(a == b);


    public bool Equals(ValueObject? other) => other is not null && GetAtomicValues().SequenceEqual(other.GetAtomicValues());


    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return obj is ValueObject valueObject && GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }


    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (var obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }


    protected abstract IEnumerable<object> GetAtomicValues();
}