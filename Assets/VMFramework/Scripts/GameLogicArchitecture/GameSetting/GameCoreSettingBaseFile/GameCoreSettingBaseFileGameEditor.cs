#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingBaseFile : IGameEditorToolBarProvider, IGameEditorContextMenuProvider
    {
        IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> IGameEditorToolBarProvider.
            GetToolbarButtons()
        {
            yield return new(GameEditorNames.openScriptButtonName, this.OpenScriptOfObject);
            yield return new(GameEditorNames.saveButtonName, this.EnforceSave);
        }
    }
}
#endif