using FluentValidation.Validators;

namespace Sawoodamo.API.Utilities;

public static class Constants
{
    public static class CustomClaimTypes
    {
        public const string Uid = "uid";
        public const string EmailConfirmed = "emailConfirmed";
    }

    public static class AuditTrails
    {
        public const int EntityTypeMaxLength = 100;
        public const int TimeStampMaxLength = 100;
        public const int OperationTypeMaxLength = 10;

    }

    public static class Other
    {
        public const int GuidMaxLength = 36;
        public const int PhoneNumberMaxLength = 36;
        public const int SlugMaxLength = 255;
        public const int IdMaxLength = 68;
    }

    public static class User
    {
        public const int NameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int EmailMaxLength = 50;
    }

    public static class Product
    {
        public const int NameMaxLength = 100;
        public const int NameMinLength = 2;
        public const int ShortDescriptionMaxLength = 250;
        public const int FullDescriptionMaxLength = 1000;
    }

    public static class ProductImage
    {
        public const int UrlMaxLength = 200;
    }

    public static class Category
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
    }

    public static class Slug
    {
        public const int MaxLength = 255;
        public const int MinLength = 1;
    }

    public static class ProductSpec
    {
        public const int SpecNameMaxLength = 50;
        public const int SpecValueMaxLength = 255;
    }

    public static class AsyncValidatorErrorCodes
    {
        public const string DuplicateSlug = "DuplicateSlug";
        public const string DuplicateName = "DuplicateName";
        public const int ErrorCode = 404;
    }
    
    public static class Roles
    {
        public const string Admin = "Admin";
    }
}
public static class ErrorMessageGenerator
{
    public const string InternalServerError = "Innternal server error";
    
    public static string Length(string propertyName, int minLength, int maxLength) =>
        $"{propertyName} must contain {minLength} - {maxLength} characters";

    public static string Invalid(string propertyName) =>
        $"{propertyName} is invalid";

    public static string Required(string propertyName) =>
        $"{propertyName} is required";

    public static string InUse(string propertyName) =>
        $"{propertyName} is not free";

    public static string MaxLength(string propertyName, int length) =>
        $"{propertyName} must be shorter than {length} characters";
}