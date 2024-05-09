using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UIPanelProcedureConfig : BaseConfigClass, IIDOwner
    {
        [LabelText("流程")]
        [ProcedureIDValueDropdown]
        [IsNotNullOrEmpty]
        public string procedureID;
        
        [LabelText("进入时自动打开的唯一UI面板")]
        [UIPresetIDValueDropdown(true)]
        [DisallowDuplicateElements]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<string> uniqueUIPanelAutoOpenListOnEnter = new();
        
        [LabelText("进入时自动关闭的UI面板")]
        [UIPresetIDValueDropdown]
        [DisallowDuplicateElements]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<string> uiPanelAutoCloseListOnEnter = new();
        
        [LabelText("退出时自动打开的唯一UI面板")]
        [UIPresetIDValueDropdown(true)]
        [DisallowDuplicateElements]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<string> uniqueUIPanelAutoOpenListOnExit = new();
        
        [LabelText("退出时自动关闭的UI面板")]
        [UIPresetIDValueDropdown]
        [DisallowDuplicateElements]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<string> uiPanelAutoCloseListOnExit = new();

        string IIDOwner<string>.id => procedureID;
    }
}