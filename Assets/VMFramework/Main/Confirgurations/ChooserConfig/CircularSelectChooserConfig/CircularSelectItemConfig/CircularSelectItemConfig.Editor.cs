#if UNITY_EDITOR
using System;
using VMFramework.Core;
using Object = UnityEngine.Object;

namespace VMFramework.Configuration
{
    public partial class CircularSelectItemConfig<T>
    {
        [NonSerialized]
        public int index;

        private string valueLabel => "第" + index + "个";
        
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            if (value == null)
            {
                if (typeof(T).IsClass &&
                    typeof(T).IsSubclassOf(typeof(Object)) == false)
                {
                    value = (T)typeof(T).CreateInstance();
                }
            }

            times = times.ClampMin(1);
        }
    }
}
#endif