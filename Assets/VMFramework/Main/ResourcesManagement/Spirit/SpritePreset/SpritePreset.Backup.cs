#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset
    {
        [LabelText("备份路径"), LabelWidth(60), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        [SerializeField]
        [DisplayAsString]
        private string backupAssetPath;

        [LabelText("备份名称"), LabelWidth(60), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        [SerializeField]
        [DisplayAsString]
        private string backupAssetName;
        
        [Button("生成备份"), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        public void GenerateBackup()
        {
            if (sprite != null)
            {
                backupAssetPath = AssetDatabase.GetAssetPath(sprite).GetDirectoryPath();

                backupAssetName = sprite.name;
            }
        }

        [Button("从备份恢复"), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        public void RestoreFromBackup()
        {
            if (string.IsNullOrEmpty(backupAssetPath) == false &&
                string.IsNullOrEmpty(backupAssetName) == false)
            {
                sprite = backupAssetName.FindAssetOfName<Sprite>(backupAssetPath);
            }
        }
    }
}
#endif