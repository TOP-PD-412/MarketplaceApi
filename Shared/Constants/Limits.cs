namespace Shared.Constants;

public static class Limits
{
    public static class Product
    {
        public static class Name
        {
            public const int MaxLength = 63;
        }
        
        public static class Description
        {
            public const int MaxLength = 1023;
        }
    }

    public static class User
    {
        public static class Name
        {
            public const int MaxLength = 63;
        }
    }
}