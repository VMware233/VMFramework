using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Properties
{
    [PreviewComposite]
    public struct BaseFloatProperty<TOwner> : IFormattable, IComparable<BaseFloatProperty<TOwner>>
        where TOwner : class
    {
        public readonly TOwner owner;

        private float _value;

        [ShowInInspector]
        [DelayedProperty]
        public float value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldHealth = _value;
                _value = value;
                OnValueChanged?.Invoke(owner, oldHealth, _value);
            }
        }

        public event Action<TOwner, float, float> OnValueChanged;

        public BaseFloatProperty(TOwner owner, float value)
        {
            this.owner = owner;
            _value = value;
            OnValueChanged = null;
        }

        #region To String

        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }

        public readonly override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        public static implicit operator float(BaseFloatProperty<TOwner> property) => property.value;

        public readonly int CompareTo(BaseFloatProperty<TOwner> other)
        {
            return _value.CompareTo(other._value);
        }
    }

    [PreviewComposite]
    public struct BaseFloatProperty : IFormattable, IComparable<BaseFloatProperty>
    {
        private float _value;

        [ShowInInspector]
        [DelayedProperty]
        public float value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldHealth = _value;
                _value = value;
                OnValueChanged?.Invoke(oldHealth, _value);
            }
        }

        public event Action<float, float> OnValueChanged;

        public BaseFloatProperty(float value)
        {
            _value = value;
            OnValueChanged = null;
        }

        #region To String

        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }

        public readonly override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        public static implicit operator float(BaseFloatProperty property) =>
            property.value;

        public readonly int CompareTo(BaseFloatProperty other)
        {
            return _value.CompareTo(other._value);
        }
    }
}