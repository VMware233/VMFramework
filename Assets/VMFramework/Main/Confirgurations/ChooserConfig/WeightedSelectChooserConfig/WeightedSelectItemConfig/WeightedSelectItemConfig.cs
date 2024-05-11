using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectItemConfig<T> : BaseConfigClass, IWeightedSelectItem<T>, ICloneable
    {
        [HideLabel]
        public T value;

        [LabelText("占比"), LabelWidth(30)]
        [HorizontalGroup]
        public int ratio;

        [LabelText("概率"), LabelWidth(30)]
        [SuffixLabel("%", Overlay = true)]
        [HorizontalGroup]
        [DisplayAsString]
        [JsonIgnore]
        public float probability;

        [HideLabel]
        [HorizontalGroup]
        [DisplayAsString]
        [GUIColor("@Color.yellow")]
        [JsonIgnore]
        public string tag;

        public object Clone()
        {
            return new WeightedSelectItemConfig<T>()
            {
                value = value,
                ratio = ratio,
            };
        }

        T IWeightedSelectItem<T>.value => value;

        float IWeightedSelectItem<T>.weight => ratio;
    }
}