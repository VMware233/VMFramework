#if UNITY_EDITOR
using System.Runtime.CompilerServices;

namespace VMFramework.Editor
{
    public static class GameItemBaseTypeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetName(this GameItemBaseType type)
        {
            return type.ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetInterfaceName(this GameItemBaseType type)
        {
            return "I" + type;
        }
    }
}
#endif