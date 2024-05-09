using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using VMFramework.Core.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

#if UNITY_EDITOR
#endif

namespace VMFramework.Configuration
{
    public interface IKCubeConfig<TPoint> : IKCube<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public new TPoint min { get; set; }

        public new TPoint max { get; set; }

        TPoint IKCube<TPoint>.min
        {
            get => min;
            init => min = value;
        }

        TPoint IKCube<TPoint>.max
        {
            get => max;
            init => max = value;
        }

        //public TPoint size { get; }

        //public TPoint pivot { get; }

        //public bool Contains(TPoint pos);

        //public TPoint GetRandomPoint();
    }

    public interface IKCubeFloatConfig<TPoint> : IKCubeConfig<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public TPoint extents { get; }
    }

    public interface IKCubeIntegerConfig<TPoint> : IKCubeConfig<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {

    }

    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [JsonObject(MemberSerialization.OptIn)]
    [PreviewComposite]
    [TypeValidation]
    [Serializable]
    public abstract class KCubeConfig<TPoint> : IKCubeConfig<TPoint>, ICloneable,
        IMinimumValueProvider, IMaximumValueProvider, ITypeValidationProvider
        where TPoint : struct, IEquatable<TPoint>
    {
        protected const string WRAPPER_GROUP = "WrapperGroup";

        protected const string MIN_MAX_VALUE_GROUP =
            WRAPPER_GROUP + "/MinMaxValueGroup";

        protected const string INFO_VALUE_GROUP = WRAPPER_GROUP + "/InfoValueGroup";

        protected virtual string pointName => "点";

        protected virtual string sizeName => "尺寸";

        protected virtual bool requireCheckSize => true;

        [LabelText(@"@""最小"" + pointName"), HorizontalGroup(WRAPPER_GROUP),
         VerticalGroup(MIN_MAX_VALUE_GROUP)]
        [JsonProperty]
        public TPoint min;

        [LabelText(@"@""最大"" + pointName"), VerticalGroup(MIN_MAX_VALUE_GROUP)]
        [InfoBox(@"@""最大"" + pointName + ""不能小于最小"" + pointName",
            InfoMessageType.Error, nameof(displayMaxLessThanMinError))]
        [JsonProperty]
        public TPoint max;

        [LabelText("@" + nameof(sizeName)), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        [LabelText("中心"), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint pivot
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #region GUI

        private bool displayMaxLessThanMinError => requireCheckSize && max
            .AnyNumberBelow(min);

        #endregion

        #region Cloneable

        public abstract object Clone();

        #endregion

        #region IKCube

        TPoint IKCubeConfig<TPoint>.min
        {
            get => min;
            set => min = value;
        }

        TPoint IKCubeConfig<TPoint>.max
        {
            get => max;
            set => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint ClampMin(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint ClampMax(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint GetRelativePos(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool Contains(TPoint pos);

        #endregion

        #region To String

        public override string ToString()
        {
            return $"[{min}, {max}]";
        }

        #endregion

        #region Minimum & Maximum Value Provider

        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            min = min.ClampMin(minimum);
            max = max.ClampMin(minimum);
        }

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            min = min.ClampMax(maximum);
            max = max.ClampMax(maximum);
        }

        #endregion

        #region Type Validation Provider

#if UNITY_EDITOR
        IEnumerable<ValidationResult> ITypeValidationProvider.
            GetValidationResults(GUIContent label)
        {
            if (displayMaxLessThanMinError)
            {
                yield return new($"{sizeName}为负值，最大{pointName}不能小于最小{pointName}",
                    ValidateType.Error);
            }
        }
#endif

        #endregion
    }

    public abstract class KCubeFloatConfig<TPoint> : KCubeConfig<TPoint>,
        IKCubeFloatConfig<TPoint>, IKCubeFloat<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        protected virtual string extentsName => "半径";

        [LabelText("@" + nameof(extentsName)), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint extents
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
    }

    public abstract class KCubeIntegerConfig<TPoint> : KCubeConfig<TPoint>,
        IKCubeIntegerConfig<TPoint>, IKCubeInteger<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract int GetPointsCount();
    }

    //#region ShapeSet

    //[Serializable]
    //[HideDuplicateReferenceBox]
    //[HideReferenceObjectPicker]
    //public class RangeIntegerSet
    //{
    //    public enum FindStrategy
    //    {
    //        NormalNearest,
    //        BiggerFirst,
    //        SmallerFirst
    //    }

    //    [LabelText("最大值")]
    //    [ShowInInspector]
    //    public int maxPos => set.Count == 0 ? int.MinValue : set[^1].maxPos;
    //    [LabelText("最小值")]
    //    [ShowInInspector]
    //    public int minPos => set.Count == 0 ? int.MaxValue : set[0].minPos;

    //    [LabelText("集合")]
    //    [ShowInInspector]
    //    private readonly List<RangeIntegerConfig> set = new();

    //    public bool Contains(int pos)
    //    {
    //        return set.Any(range => range.Contains(pos));
    //    }

    //    [Button("随机添加点"), ShowIf("isDebugging")]
    //    public void RandomAddPoint(int count, int start, int end)
    //    {
    //        foreach (var pos in count.GenerateUniqueIntegers(start, end))
    //        {
    //            AddPoint(pos);
    //        }
    //    }

    //    [Button("添加点"), ShowIf("isDebugging")]
    //    public void AddPoint(int pos)
    //    {
    //        List<RangeIntegerConfig> toUnions = new();

    //        int i;
    //        for (i = 0; i < set.Count; i++)
    //        {
    //            var range = set[i];

    //            if (pos < range.minPos - 1)
    //            {
    //                break;
    //            }
    //            if (pos >= range.minPos - 1 && pos <= range.maxPos)
    //            {
    //                toUnions.Add(range);
    //                break;
    //            }

    //            if (pos == range.maxPos + 1)
    //            {
    //                toUnions.Add(range);
    //            }
    //        }

    //        if (toUnions.Count <= 0)
    //        {
    //            set.Insert(i, new RangeIntegerConfig(pos, pos));
    //        }
    //        else
    //        {
    //            var first = toUnions[0];

    //            if (first.TryUnion(pos) == false)
    //            {
    //                Note.note.Error($"异常, pos:{pos}, first:{first}, set:{set}");
    //            }

    //            for (var index = 1; index < toUnions.Count; index++)
    //            {
    //                var range = toUnions[index];

    //                if (first.TryUnion(range) == false)
    //                {
    //                    Note.note.Error($"异常, first:{first}, other:{range}, set:{set}");
    //                }

    //                set.Remove(range);
    //            }
    //        }
    //    }

    //    [Button("随机添加范围"), ShowIf("isDebugging")]
    //    public void RandomAddRange(int count, int start, int end)
    //    {
    //        foreach (var range in count.SeveralRandomRangeInteger(start, end))
    //        {
    //            AddRange(range);
    //        }
    //    }

    //    [Button("添加范围"), ShowIf("isDebugging")]
    //    public void AddRange(int start, int end)
    //    {
    //        AddRange(new(start, end));
    //    }

    //    public void AddRange(RangeIntegerConfig other)
    //    {
    //        List<RangeIntegerConfig> toUnions = new();

    //        int i;
    //        for (i = 0; i < set.Count; i++)
    //        {
    //            var range = set[i];

    //            if (range.minPos > other.maxPos + 1)
    //            {
    //                break;
    //            }

    //            if (range.maxPos >= other.minPos - 1)
    //            {
    //                toUnions.Add(range);
    //            }
    //        }

    //        if (toUnions.Count <= 0)
    //        {
    //            set.Insert(i, other);
    //        }
    //        else
    //        {
    //            var first = toUnions[0];

    //            if (first.TryUnion(other) == false)
    //            {
    //                Note.note.Error($"异常, other:{other}, first:{first}, set:{set}");
    //            }

    //            for (var index = 1; index < toUnions.Count; index++)
    //            {
    //                var range = toUnions[index];

    //                if (first.TryUnion(range) == false)
    //                {
    //                    Note.note.Error($"异常, first:{first}, other:{range}, set:{set}");
    //                }

    //                set.Remove(range);
    //            }
    //        }
    //    }

    //    [Button("找最近的最大点"), ShowIf("isDebugging")]
    //    public bool TryFindNearestMaxPos(int origin, out int result,
    //        FindStrategy whileContained = FindStrategy.NormalNearest, 
    //        FindStrategy whileNotContained = FindStrategy.NormalNearest)
    //    {
    //        if (set.Count <= 0)
    //        {
    //            Debug.LogWarning("set为空");
    //            result = 0;
    //            return false;
    //        }

    //        var strategy = Contains(origin) switch
    //        {
    //            true => whileContained,
    //            false => whileNotContained
    //        };

    //        switch (strategy)
    //        {
    //            case FindStrategy.NormalNearest:

    //                int nearestMaxPos = 0;
    //                int minDistance = int.MaxValue;
    //                foreach (var range in set)
    //                {
    //                    if (range.maxPos >= origin)
    //                    {
    //                        if ((range.maxPos - origin) < minDistance)
    //                        {
    //                            nearestMaxPos = range.maxPos;
    //                        }

    //                        break;
    //                    }

    //                    int distance = origin - range.maxPos;

    //                    if (distance < minDistance)
    //                    {
    //                        minDistance = distance;
    //                        nearestMaxPos = range.maxPos;
    //                    }
    //                }

    //                result = nearestMaxPos;
    //                return true;

    //            case FindStrategy.BiggerFirst:

    //                int smallerMaxPos = 0;
    //                foreach (var range in set)
    //                {
    //                    if (range.maxPos >= origin)
    //                    {
    //                        result = range.maxPos;
    //                        return true;
    //                    }

    //                    smallerMaxPos = range.maxPos;
    //                }

    //                result = smallerMaxPos;
    //                return true;

    //            case FindStrategy.SmallerFirst:

    //                int biggerMaxPos = 0;
    //                for (var i = set.Count - 1; i >= 0; i--)
    //                {
    //                    var range = set[i];
    //                    if (range.maxPos <= origin)
    //                    {
    //                        result = range.maxPos;
    //                        return true;
    //                    }

    //                    biggerMaxPos = range.maxPos;
    //                }

    //                result = biggerMaxPos;
    //                return true;

    //            default:
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }

    //    //public void RemovePoint(int point)
    //    //{
    //    //    RangeIntegerConfig toCut = null;
    //    //    foreach (var range in set)
    //    //    {

    //    //    }
    //    //}

    //    public override string ToString()
    //    {
    //        return set.ToString(",");
    //    }
    //}

    //#endregion
}