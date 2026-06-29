namespace Shared.Constants;

public static class Config
{
    public static class Envs
    {
        public static class Db
        {
            public const string Connection = "DB_CONNECTION";
        }
        
        public const string Environment = "ASPNETCORE_ENVIRONMENT";
        public const string Local = "Local";
        public const string Container = "DOTNET_RUNNING_IN_CONTAINER";
    }
}