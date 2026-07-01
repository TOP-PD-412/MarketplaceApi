namespace Shared.Constants;

public static class Config
{
    public static class Values
    {
        public const string True = "true";
        public const string False = "false";
    }

    public static class Envs
    {
        public static class Db
        {
            public const string Connection = "DB_CONNECTION";
        }
        
        public static class Jwt
        {
            public const string Issuer = "JWT";
            public const string Audience = "JWT_AUDIENCE";
            public const string Secret = "JWT_SECRET";
        }

        public const string Environment = "ASPNETCORE_ENVIRONMENT";

        public static class Container
        {
            public const string IsRunning = "DOTNET_RUNNING_IN_CONTAINER";
        }
    }
}