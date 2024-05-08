using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        public class CircularSelectItem : BaseConfigClass, ICloneable
        {
            [HideInInspector]
            public int index;

            [LabelText(@"@""第"" + index.ToString() + ""个""")]
            [JsonProperty]
            public T value;

            [LabelText("循环次数")]
            [MinValue(1)]
            [JsonProperty]
            public int times;

            #region GUI

            protected override void OnInspectorInit()
            {
                base.OnInspectorInit();

                if (value == null)
                {
                    if (typeof(T).IsClass &&
                        typeof(T).IsSubclassOf(typeof(UnityEngine.Object)) == false)
                    {
                        value = (T)typeof(T).CreateInstance();
                    }
                }
            }

            #endregion

            public object Clone()
            {
                return new CircularSelectItem()
                {
                    value = value,
                    times = times,
                };
            }
        }
    }
}