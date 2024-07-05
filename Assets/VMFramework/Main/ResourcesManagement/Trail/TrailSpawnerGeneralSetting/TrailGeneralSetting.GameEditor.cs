#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public partial class TrailGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Trail Preset";
    }
}
#endif