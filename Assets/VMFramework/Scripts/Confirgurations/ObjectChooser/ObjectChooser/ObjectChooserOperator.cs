using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(ObjectChooser<T> chooser)
        {
            if (chooser == null)
            {
                Debug.LogWarning("chooser is Null");
                return default;
            }

            return chooser.GetValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ObjectChooser<T>(T value)
        {
            return new()
            {
                isRandomValue = false,
                value = value
            };
        }
    }
}