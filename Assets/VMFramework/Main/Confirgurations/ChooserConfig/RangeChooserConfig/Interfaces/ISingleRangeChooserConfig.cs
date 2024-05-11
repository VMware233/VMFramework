using System;
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface ISingleRangeChooserConfig<T> : IRangeChooserConfig<T>, ISingleChooserConfig<T>, IKCubeConfig<T>
        where T : struct, IEquatable<T>
    {
        IChooser<T> IChooserConfig<T>.GenerateNewObjectChooser()
        {
            return new RangeChooser<T>(this);
        }

        IEnumerable<T> IChooserConfig<T>.GetAvailableValues()
        {
            yield return min;
            yield return max;
        }

        void IChooserConfig<T>.SetAvailableValues(Func<T, T> setter)
        {
            min = setter(min);
            max = setter(max);
        }
    }
}