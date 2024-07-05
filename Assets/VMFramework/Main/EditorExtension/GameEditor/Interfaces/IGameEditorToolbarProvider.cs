#if UNITY_EDITOR

using System.Collections.Generic;

namespace VMFramework.Editor.GameEditor
{
    public interface IGameEditorToolbarProvider
    {
        public IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield break;
        }
    }
}
#endif