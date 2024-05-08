using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public sealed partial class Vector3Setter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetMaxValue()
        {
            if (asVector2D == false)
            {
                return setter3D.GetMaxValue();
            }

            return setter2D.GetMaxValue().InsertAs(0, planeType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMaxValue(Vector3 maxValue)
        {
            if (asVector2D == false)
            {
                setter3D.SetMaxValue(maxValue);
            }
            
            setter2D.SetMaxValue(maxValue.ExtractAs(planeType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetMinValue()
        {
            if (asVector2D == false)
            {
                return setter3D.GetMinValue();
            }

            return setter2D.GetMinValue().InsertAs(0, planeType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinValue(Vector3 minValue)
        {
            if (asVector2D == false)
            {
                setter3D.SetMinValue(minValue);
            }
            
            setter2D.SetMinValue(minValue.ExtractAs(planeType));
        }
    }
}