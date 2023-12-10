namespace Sawoodamo.API.Utilities;

public static class Constants
{
    public static class CustomClaimTypes
    {
        public const string Uid = "uid";
        public const string EmailConfirmed = "emailConfirmed";
    }

    public static class Other
    {
        public const int GuidMaxLength = 36;
        public const int PhoneNumberMaxLength = 36;
        public const int SlugMaxLength = 255;
    }

    public static class User
    {
        public const int NameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int EmailMaxLength = 50;
    }

    public static class Product
    {
        public const int ProductNameMaxLength = 100;
        public const int ProductShortDescriptionMaxLength = 250;
        public const int ProductFullDescriptionMaxLength = 1000;
    }

    public static class Category
    {
        public const int CategoryNameMaxLength = 50;
    }
}
