using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Configuration
{
    public abstract class KCubeIntegerConfig<TPoint> : KCubeConfig<TPoint>, IKCubeIntegerConfig<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public abstract int Count { get; }
        
        public abstract IEnumerator<TPoint> GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}