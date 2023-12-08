using FluentValidation.Results;

namespace Sawoodamo.API.Utilities.Validation;

public sealed class ValidationException(IEnumerable<ValidationFailure> failures) : Exception("One or more validation failures has occurred.")
{
    public IReadOnlyCollection<Error> Errors { get; } = failures
            .Distinct()
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();
}

public sealed class Error(string code, string message) : ValueObject
{
    public string Code { get; } = code;

    public string Message { get; } = message;

    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }
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