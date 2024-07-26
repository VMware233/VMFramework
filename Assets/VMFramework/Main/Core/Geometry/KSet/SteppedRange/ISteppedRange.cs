using System;

namespace VMFramework.Core
{
    public interface ISteppedRange<TPoint> : IEnumerableKSet<TPoint>, IMinMaxOwner<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public TPoint step { get; }
    }
}