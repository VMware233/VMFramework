using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework
{
    public sealed partial class MouseEventGeneralSetting : GeneralSettingBase
    {
        //[LabelText("绑定相机")]
        //[SceneObjectsOnly, Required]
        //public Camera bindCamera;

        [LabelText("2D检测的射线长度")]
        [Range(0, 100)]
        public float detectDistance2D = 100;
    }
}
