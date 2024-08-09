using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly struct UniformlySpacedRangeVector4 : ISteppedRange<Vector4>
    {
        public readonly Vector4 min;
        public readonly Vector4 max;
        public readonly int count;
        
        public Vector4 step => count > 0 ? (max - min) / (count - 1) : Vector4.zero;
        
        public UniformlySpacedRangeVector4(Vector4 min, Vector4 max, int count)
        {
            this.min = min;
            this.max = max;
            this.count = count;
        }
        
        Vector4 IMinMaxOwner<Vector4>.Min
        {
            get => min;
            init => min = value;
        }

        Vector4 IMinMaxOwner<Vector4>.Max
        {
            get => max;
            init => max = value;
        }
        
        int IReadOnlyCollection<Vector4>.Count => count;

        public bool Contains(Vector4 pos)
        {
            if (count <= 0)
            {
                return false;
            }

            if (count == 1)
            {
                return pos == (max + min) / 2;
            }

            if (count == 2)
            {
                return pos == min || pos == max;
            }
            
            var offset = pos - min;
            return offset.x % step.x == 0 && offset.y % step.y == 0 && offset.z % step.z == 0 && offset.w % step.w == 0;
        }
        
        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Vector4> GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public struct Enumerator : IEnumerator<Vector4>
        {
            private readonly UniformlySpacedRangeVector4 range;
            private readonly Vector4 step;
            private Vector4 x;
            private int index;

            public Enumerator(UniformlySpacedRangeVector4 range)
            {
                this.range = range;
                step = range.step;
                x = range.min - step;
                index = -1;
            }

            public Vector4 Current => x;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (range.count <= 0)
                {
                    return false;
                }
                
                index++;

                if (range.count == 1)
                {
                    x = (range.max + range.min) / 2;
                    return index < 1;
                }
                
                x += step;
                return index < range.count;
            }

            public void Reset()
            {
                x = range.min - range.step;
            }

            public void Dispose() { }
        }

        #endregion
    }
}