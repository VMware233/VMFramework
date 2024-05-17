#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;

namespace VMFramework.UI
{
    public partial class UIPanelPreset : IGameEditorToolBarProvider
    {
        public IEnumerable<IGameEditorToolBarProvider.ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(GameEditorNames.openControllerScriptButtonName, OpenControllerScript);
        }
    }
}
#endif