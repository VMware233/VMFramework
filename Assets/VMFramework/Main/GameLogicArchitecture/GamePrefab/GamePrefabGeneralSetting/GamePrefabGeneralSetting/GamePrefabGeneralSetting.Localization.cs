#if UNITY_EDITOR
using ParrelSync;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        public override bool localizationEnabled =>
            baseGamePrefabType.IsDerivedFrom<ILocalizedGamePrefab>(false);

        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);
            
            if (ClonesManager.IsClone())
            {
                return;
            }

            foreach (var gamePrefabWrapper in initialGamePrefabWrappers)
            {
                if (gamePrefabWrapper == null)
                {
                    continue;
                }
                
                gamePrefabWrapper.AutoConfigureLocalizedString(new()
                {
                    defaultTableName = settings.defaultTableName,
                    save = false
                });
            }

            if (settings.save)
            {
                this.EnforceSave();
            }
            else
            {
                this.SetEditorDirty();
            }
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            foreach (var gamePrefabWrapper in initialGamePrefabWrappers)
            {
                gamePrefabWrapper.SetKeyValueByDefault();
            }
        }
    }
}
#endif