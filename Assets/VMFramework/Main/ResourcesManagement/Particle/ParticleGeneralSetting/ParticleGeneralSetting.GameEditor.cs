#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticleGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Particle Preset";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Flower1;
    }
}
#endif