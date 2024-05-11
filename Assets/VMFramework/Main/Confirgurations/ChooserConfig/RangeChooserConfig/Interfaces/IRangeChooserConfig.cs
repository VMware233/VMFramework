using System;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IRangeChooserConfig<T> : IChooserConfig<T>, IKCube<T> 
        where T : struct, IEquatable<T>
    {
        
    }
}