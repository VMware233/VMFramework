#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectItemConfig<T>
    {
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
    }
}
#endif