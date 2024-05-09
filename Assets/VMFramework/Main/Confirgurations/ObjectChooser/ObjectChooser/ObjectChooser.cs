using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;
using Newtonsoft.Json;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public interface IValueSetter<out T>
    {
        public T GetValue();

        public IEnumerable<T> GetCurrentValues();
    }

    [JsonObject(MemberSerialization.OptIn)]
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [PreviewComposite]
    [Serializable]
    public partial class ObjectChooser<T> : BaseConfigClass, IValueSetter<T>, ICloneable
    {
        #region Chooser Types

        public const string SINGLE_VALUE = "Single Value";
        public const string WEIGHTED_SELECT = "Weighted Select";
        public const string CIRCULAR_SELECT = "Circular Select";

        #endregion

        protected virtual IEnumerable<ValueDropdownItem<string>> allRandomTypes =>
            new ValueDropdownList<string>()
            {
                { "权值选择", WEIGHTED_SELECT },
                { "循环选择", CIRCULAR_SELECT }
            };

        protected virtual IEnumerable<ValueDropdownItem<string>> allFixedTypes =>
            new ValueDropdownList<string>()
            {
                { $"单个{valueName}", SINGLE_VALUE }
            };

        #region Fields

        [HideLabel, PropertyOrder(-999)]
        [ToggleButtons(@"@""随机"" + valueName", @"@""固定"" + valueName")]
        [OnValueChanged(nameof(OnIsRandomValueChanged))]
        [JsonProperty(Order = -101)]
        public bool isRandomValue = false;
        
        [LabelText(@"@""固定"" + valueName + ""种类"""), PropertyOrder(-888)]
        [OnValueChanged(nameof(OnIsRandomValueChanged))]
        [ValueDropdown(nameof(allFixedTypes))]
        [ShowIf(nameof(displayFixedTypeChooser))]
        [JsonProperty(Order = -100)]
        public string fixedType;

        [HideLabel]
        [ShowIf(nameof(isSingleValue))]
        [JsonProperty(Order = -98)]
        public T value;
        
        [LabelText(@"@""随机"" + valueName + ""种类"""), PropertyOrder(-888)]
        [OnValueChanged(nameof(OnIsRandomValueChanged))]
        [ValueDropdown(nameof(allRandomTypes))]
        [ShowIf(nameof(displayRandomTypeChooser))]
        [JsonProperty(Order = -99)]
        public string randomType;

        #endregion

        #region Properties
        
        public bool isSingleValue => isRandomValue == false && fixedType == SINGLE_VALUE;

        public bool isWeightedSelect => isRandomValue && randomType == WEIGHTED_SELECT;

        public bool isCircularSelect => isRandomValue && randomType == CIRCULAR_SELECT;

        #endregion

        #region GUI

        protected virtual string valueName => "";

        protected virtual bool displayRandomTypeChooser => allRandomTypes.Count() > 1 && isRandomValue;

        protected virtual bool displayFixedTypeChooser => allFixedTypes.Count() > 1 && isRandomValue == false;

        #region GUI Event

        protected virtual void OnIsRandomValueChanged()
        {
            OnInspectorInit();
        }

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            if (randomType.IsNullOrEmptyAfterTrim() ||
                GetRandomTypes().Contains(randomType) == false)
            {
                randomType = GetDefaultRandomType();
            }

            if (fixedType.IsNullOrEmptyAfterTrim() ||
                GetFixedTypes().Contains(fixedType) == false)
            {
                fixedType = GetDefaultFixedType();
            }

            weightedSelectItems ??= new();
            circularSelectItems ??= new();

            if (typeof(T).IsDerivedFrom<IBaseConfigClass>(false))
            {
                value ??= (T)typeof(T).CreateInstance();

                foreach (var item in weightedSelectItems)
                {
                    item.value ??= (T)typeof(T).CreateInstance();
                }

                foreach (var item in circularSelectItems)
                {
                    item.value ??= (T)typeof(T).CreateInstance();
                }
            }

            if (isWeightedSelect)
            {
                OnWeightedSelectItemsChanged();
            }
        }

        #endregion

        #endregion

        #region Fixed & Random Types

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetDefaultRandomType()
        {
            return allRandomTypes.First().Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetDefaultFixedType()
        {
            return allFixedTypes.First().Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> GetRandomTypes()
        {
            return allRandomTypes.Select(item => item.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<string> GetFixedTypes()
        {
            return allFixedTypes.Select(item => item.Value);
        }

        #endregion

        #region Get Value

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T GetFixedValue()
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T GetRandomValue()
        {
            switch (randomType)
            {
                case WEIGHTED_SELECT:

                    return GetWeightedSelectValue();

                case CIRCULAR_SELECT:

                    return GetCircularSelectValue();

                default:
                    Debug.LogWarning($"randomType的值错误:{randomType}");
                    return value;

            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue()
        {
            return isRandomValue == false ? GetFixedValue() : GetRandomValue();
        }

        #endregion

        #region Get Current Values

        protected virtual IEnumerable<T> GetCurrentFixedValues()
        {
            yield return fixedType switch
            {
                SINGLE_VALUE => value,
                _ => throw new ArgumentException()
            };
        }

        protected virtual IEnumerable<T> GetCurrentRandomValues()
        {
            return randomType switch
            {
                WEIGHTED_SELECT => GetCurrentWeightedSelectValues(),
                CIRCULAR_SELECT => GetCurrentCircularSelectValues(),
                _ => throw new ArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetCurrentValues()
        {
            if (isRandomValue)
            {
                return GetCurrentRandomValues();
            }

            return GetCurrentFixedValues();
        }

        #endregion

        #region To String

        protected virtual string ValueToString(T value)
        {
            if (value == null)
            {
                return "null";
            }

            if (value is IEnumerable enumerable)
            {
                return enumerable.Cast<object>().Join(", ");
            }
            
            return value.ToString();
        }

        public override string ToString()
        {
            if (isRandomValue)
            {
                switch (randomType)
                {
                    case WEIGHTED_SELECT:
                        return WeightedSelectItemsToString();
                    case CIRCULAR_SELECT:
                        return CircularSelectItemsToString();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return ValueToString(value);
        }

        #endregion

        #region Clone

        public virtual object Clone()
        {
            var clone = (ObjectChooser<T>)GetType().CreateInstance();
            clone.isRandomValue = isRandomValue;
            clone.fixedType = fixedType;
            clone.randomType = randomType;
            clone.value = value.Clone();
            clone.weightedSelectItems = weightedSelectItems.Clone();
            clone.startCircularIndex = startCircularIndex;
            clone.pingPong = pingPong;
            clone.circularSelectItems = circularSelectItems.Clone();
            clone.currentCircularIndex = 0;
            clone.currentCircularTimes = 1;
            clone.loopForward = true;
            return clone;
        }

        #endregion
    }
}