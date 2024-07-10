#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure 
{
    public partial class DefaultGlobalScenesGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Default Global Scenes";
    }
}
#endif