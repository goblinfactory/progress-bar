namespace Konsole.Internal
{
    internal static class IntegerExtensions
    {
        internal static int Max(this int src, int max)
        {
            return src > max ? max : src;
        }
    }
}
