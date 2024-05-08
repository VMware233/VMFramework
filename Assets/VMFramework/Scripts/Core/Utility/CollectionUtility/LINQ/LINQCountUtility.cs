using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Count

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count<T>(this IEnumerable<T> enumerable, T itemToCount)
        {
            return enumerable.Count(item => item.Equals(itemToCount));
        }

        #endregion

        #region Unique Count

        public static int UniqueCount<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Distinct().Count();
        }

        #endregion
    }
}