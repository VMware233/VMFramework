#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class DefaultContainerItemConsumptionConfig<TItem, TItemPrefab>
    {
        protected virtual IEnumerable<ValueDropdownItem> GetItemPrefabNameList()
        {
            return GamePrefabNameListQuery.GetGamePrefabNameListByType(typeof(TItemPrefab));
        }
    }
}
#endif