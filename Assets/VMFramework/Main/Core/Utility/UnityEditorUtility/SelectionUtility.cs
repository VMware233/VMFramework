#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static class SelectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SelectObject(this Object obj)
        {
            Selection.activeObject = obj;
        }
    }
}
#endif