namespace Shared.Utils;

public static class StringEx
{
    public static Uri? ToUri(this string str) =>
        Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out var url) ? url : null;
}