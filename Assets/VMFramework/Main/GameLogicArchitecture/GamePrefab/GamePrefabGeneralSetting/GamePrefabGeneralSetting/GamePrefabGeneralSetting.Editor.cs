#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        [TabGroup(TAB_GROUP_NAME, METADATA_CATEGORY)]
        [ShowInInspector]
        public string gamePrefabFolderPath =>
            EditorSetting.gamePrefabsAssetFolderPath.PathCombine(gamePrefabName);
        
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            RefreshInitialGamePrefabWrappers();
        }
        
        private void OnInitialGamePrefabWrappersChanged()
        {
            OnInspectorInit();
            
            this.EnforceSave();
        }
        
        public void AddToInitialGamePrefabWrappers(GamePrefabWrapper wrapper)
        {
            if (wrapper == null)
            {
                return;
            }
            
            if (initialGamePrefabWrappers.Contains(wrapper))
            {
                return;
            }
            
            initialGamePrefabWrappers.Add(wrapper);
            
            OnInitialGamePrefabWrappersChanged();
        }
        
        public void RemoveFromInitialGamePrefabWrappers(GamePrefabWrapper wrapper)
        {
            initialGamePrefabWrappers.Remove(wrapper);
            
            OnInitialGamePrefabWrappersChanged();
        }
    }
}
#endif