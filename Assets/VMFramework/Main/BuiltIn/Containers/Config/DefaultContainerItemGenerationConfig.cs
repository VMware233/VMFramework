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
        public IChooserConfig<int> count = new SingleValueChooserConfig<int>(1);

        public abstract TItem GenerateItem();

        public override string ToString()
        {
            return $"{itemID}, {count}";
        }
    }
}
