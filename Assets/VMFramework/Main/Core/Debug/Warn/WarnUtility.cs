using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class WarnUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarnIfNull<T>(this T obj, string name)
        {
            if (obj == null)
            {
                Debugger.LogWarning($"{name} is null.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarnIfNullOrEmpty(this string str, string name)
        {
            if (string.IsNullOrEmpty(str))
            {
                Debugger.LogWarning($"{name} is null or empty.");
            }
        }
    }
}