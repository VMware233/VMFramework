using System;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IKCubeConfig<TPoint> : IKCube<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public new TPoint min { get; set; }

        public new TPoint max { get; set; }

        TPoint IMinMaxOwner<TPoint>.Min
        {
            get => min;
            init => min = value;
        }

        TPoint IMinMaxOwner<TPoint>.Max
        {
            get => max;
            init => max = value;
        }
    }
}