#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabWrapper : IGameEditorMenuTreeNode, IGameEditorToolBarProvider, 
        IGameEditorContextMenuProvider
    {
        IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> IGameEditorToolBarProvider.GetToolbarButtons()
        {
            return GetToolbarButtons();
        }
        
        protected virtual IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(GameEditorNames.openScriptButtonName, this.OpenScriptOfObject);
            yield return new(GameEditorNames.saveButtonName, this.EnforceSave);
        }
    }
}
#endif