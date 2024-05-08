using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.Configuration
{
    public abstract partial class NumberOrVectorChooser<T, TRange> : ObjectChooser<T>, 
        INumberOrVectorValueSetter<T>
        where T : struct, IEquatable<T>
        where TRange : KCubeConfig<T>, new()
    {
        public const string RANGE_SELECT = "Range";

        protected override IEnumerable<ValueDropdownItem<string>> allRandomTypes =>
            base.allRandomTypes.Concat(new ValueDropdownList<string>
            {
                { "范围", RANGE_SELECT }
            });

        #region Fields

        [HideLabel]
        [ShowIf(nameof(isRangeSelect))]
        [JsonProperty]
        public TRange rangeValue;

        #endregion

        #region Properties

        public bool isRangeSelect => isRandomValue && randomType == RANGE_SELECT;

        #endregion

        #region GUI

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            rangeValue ??= new();
        }

        #endregion

        public override T GetRandomValue()
        {
            if (randomType == RANGE_SELECT)
            {
                return rangeValue.GetRandomPoint();
            }
            
            return base.GetRandomValue();
        }

        protected override IEnumerable<T> GetCurrentRandomValues()
        {
            if (randomType == RANGE_SELECT)
            {
                return new[] { rangeValue.min, rangeValue.max };
            }
            
            return base.GetCurrentRandomValues();
        }

        #region Clone

        public override object Clone()
        {
            var baseClone = base.Clone();
            var newInstance = (NumberOrVectorChooser<T, TRange>)baseClone;
            newInstance.rangeValue = (TRange)rangeValue?.Clone();
            return newInstance;
        }

        #endregion

        #region To String

        public override string ToString()
        {
            if (isRandomValue)
            {
                if (randomType == RANGE_SELECT)
                {
                    return
                        $"[{ValueToString(rangeValue.min)}, {ValueToString(rangeValue.max)}]";
                }
            }

            return base.ToString();
        }

        #endregion
    }
}