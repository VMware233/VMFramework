#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GlobalSettingFile : IGameEditorMenuTreeNodesProvider
    {
        bool IGameEditorMenuTreeNodesProvider.isMenuTreeNodesVisible => true;

        protected virtual IEnumerable<IGameEditorMenuTreeNode> GetAllMenuTreeNodes()
        {
            foreach (var generalSetting in GetAllGeneralSettings())
            {
                if (generalSetting is IGameEditorMenuTreeNode menuTreeNode)
                {
                    yield return menuTreeNode;
                }
            }
        }
        
        IEnumerable<IGameEditorMenuTreeNode> IGameEditorMenuTreeNodesProvider.GetAllMenuTreeNodes()
        {
            return GetAllMenuTreeNodes();
        }

        protected override IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.AUTO_FIND_SETTINGS_BUTTON_PATH, AutoFindSettings);
            yield return new(EditorNames.AUTO_FIND_AND_CREATE_SETTINGS_BUTTON_PATH,
                AutoFindAndCreateSettings);
            yield return new(EditorNames.OPEN_GLOBAL_SETTING_SCRIPT_BUTTON_PATH, OpenGlobalSettingScript);
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }
        }

        private void OpenGlobalSettingScript()
        {
            foreach (var globalSetting in GlobalSettingCollector.Collect())
            {
                if (globalSetting.globalSettingFile == null)
                {
                    continue;
                }
                
                if (globalSetting.globalSettingFile.GetType() == GetType())
                {
                    globalSetting.OpenScriptOfObject();
                }
            }
        }
    }
}
#endif