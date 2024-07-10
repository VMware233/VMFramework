#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core.Editor;

namespace VMFramework.OdinExtensions
{
    public sealed class BuildSceneNameAttributeDrawer : GeneralValueDropdownAttributeDrawer<BuildSceneNameAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return SceneUtility.GetBuildSceneNames().ToValueDropdownItems();
        }
    }
}
#endif