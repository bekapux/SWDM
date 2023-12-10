namespace Sawoodamo.API.Utilities.Validation;

public static partial class RegexValidators
{
    [GeneratedRegex("^[a-z0-9]+(?:-[a-z0-9]+)*$")]
    public static partial Regex SlugValidatorRegex();
}
