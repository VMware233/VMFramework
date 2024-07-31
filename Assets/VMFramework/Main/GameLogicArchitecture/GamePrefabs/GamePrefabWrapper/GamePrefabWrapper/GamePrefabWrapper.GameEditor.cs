#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabWrapper
    {
        protected override IEnumerable<ToolbarButtonConfig> GetToolbarButtons()
        {
            yield return new(EditorNames.OPEN_GAME_PREFAB_SCRIPT_PATH, this.OpenGamePrefabScripts);

            yield return new(EditorNames.OPEN_GAME_ITEM_SCRIPT_PATH, this.OpenGameItemScripts);
            
            foreach (var config in base.GetToolbarButtons())
            {
                yield return config;
            }
        }
    }
}
#endif