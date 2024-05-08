using System.Runtime.CompilerServices;

public static class ClassFunc
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ClassEquals<T>(this T a, T b)
    {
        var aIsNull = a == null;
        var bIsNull = b == null;

        if (aIsNull == true && bIsNull == true)
        {
            return true;
        }

        if (aIsNull == false && bIsNull == false)
        {
            return a.Equals(b);
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ClassOrStructEquals<T>(this T a, T b)
    {
        return typeof(T).IsClass ? ClassEquals(a, b) : a.Equals(b);
    }
}
