using System;

namespace VMFramework.Core
{
    public interface IMinMaxOwner<TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        public TPoint min { get; init; }
        
        public TPoint max { get; init; }
    }
}