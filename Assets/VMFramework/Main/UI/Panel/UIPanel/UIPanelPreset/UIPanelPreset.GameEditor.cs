#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.UI
{
    public partial class UIPanelPreset : IGameEditorToolbarProvider
    {
        public IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.OPEN_CONTROLLER_SCRIPT_PATH, OpenControllerScript);
        }
    }
}
#endif