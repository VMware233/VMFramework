using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using Object = UnityEngine.Object;

namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        public class WeightedSelectItem : BaseConfigClass, ICloneable
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

            #region GUI

            protected override void OnInspectorInit()
            {
                base.OnInspectorInit();

                if (value == null)
                {
                    if (typeof(T).IsClass && typeof(T).IsSubclassOf(typeof(Object)) == false)
                    {
                        value = (T)typeof(T).CreateInstance();
                    }
                }
            }

            #endregion

            public object Clone()
            {
                return new WeightedSelectItem()
                {
                    value = value,
                    ratio = ratio,
                };
            }
        }
    }
}