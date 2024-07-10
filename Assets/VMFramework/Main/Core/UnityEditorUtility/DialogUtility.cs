using System.Runtime.CompilerServices;

namespace VMFramework.Core.Editor
{
    public static class DialogUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DisplayWarningDialog(this string message)
        {
            return UnityEditor.EditorUtility.DisplayDialog("Warning", message, "OK", "Cancel");
        }
    }
}