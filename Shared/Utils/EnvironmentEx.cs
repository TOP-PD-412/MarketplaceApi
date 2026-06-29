using Shared.Constants;

namespace Shared.Utils;

public static class EnvironmentEx
{
    public static bool IsRunningInContainer() => Environment.GetEnvironmentVariable(Config.Envs.Container) == "true";
}