using System;
using UnityEngine;

namespace VMFramework.Core
{
    public static class KCubeGenericUtility
    {
        public static TPoint GetRandomPoint<TPoint>(this IKCube<TPoint> cube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube switch
            {
                IKCube<float> fCube => fCube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<int> iCube => iCube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Vector2> v2Cube => v2Cube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Vector3> v3Cube => v3Cube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Vector4> v4Cube => v4Cube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Vector2Int> v2iCube => v2iCube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Vector3Int> v3iCube => v3iCube.GetRandomPoint().ConvertTo<TPoint>(),
                IKCube<Color> cCube => cCube.GetRandomPoint().ConvertTo<TPoint>(),
                _ => throw new ArgumentException("Unsupported IKCube type")
            };
        }
    }
}
