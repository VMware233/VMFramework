using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ObjectUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindObject<T>(this string name) where T : Object
        {
            var results = Object.FindObjectsOfType<T>(true);

            return results.FirstOrDefault(result => result.name == name);
        }
    }
}