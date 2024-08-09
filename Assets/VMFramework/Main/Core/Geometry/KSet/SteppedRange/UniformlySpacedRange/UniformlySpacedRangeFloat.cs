using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Core
{
    public readonly partial struct UniformlySpacedRangeFloat : ISteppedRange<float>
    {
        public readonly float min;
        public readonly float max;
        public readonly int count;
        
        public float step => count > 0 ? (max - min) / (count - 1) : 0;
        
        public UniformlySpacedRangeFloat(float min, float max, int count)
        {
            this.min = min;
            this.max = max;
            this.count = count;
        }
        
        float IMinMaxOwner<float>.Min
        {
            get => min;
            init => min = value;
        }

        float IMinMaxOwner<float>.Max
        {
            get => max;
            init => max = value;
        }

        int IReadOnlyCollection<float>.Count => count;

        public bool Contains(float pos)
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
            
            return (pos - min) % step == 0;
        }
        
        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public struct Enumerator : IEnumerator<float>
        {
            private readonly UniformlySpacedRangeFloat range;
            private readonly float step;
            private float x;
            private int index;

            public Enumerator(UniformlySpacedRangeFloat range)
            {
                this.range = range;
                step = range.step;
                x = range.min - step;
                index = -1;
            }

            public float Current => x;

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