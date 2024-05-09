using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract partial class DefaultContainerItemGenerationConfig<TItem, TItemPrefab> : BaseConfigClass
        where TItem : IGameItem, IContainerItem
        where TItemPrefab : IGamePrefab
    {
#if UNITY_EDITOR
        [HideLabel]
        [ValueDropdown(nameof(GetItemPrefabNameList))]
        [IsNotNullOrEmpty]
#endif
        public string itemID;

        [LabelText("数量")]
        [Minimum(0)]
        public IntegerSetter count = 1;

        #region GUI

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            count ??= 1;
        }

        #endregion

        public abstract TItem GenerateItem();

        public override string ToString()
        {
            return $"{itemID}, {count}";
        }
    }
}
