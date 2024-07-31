using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class UISetting : GlobalSetting<UISetting, UISettingFile>
    {
        public static UIPanelGeneralSetting UIPanelGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.uiPanelGeneralSetting;

        public static UIPanelProcedureGeneralSetting UIPanelProcedureGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.uiPanelProcedureGeneralSetting;

        public static DebugUIPanelGeneralSetting DebugUIPanelGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.debugUIPanelGeneralSetting;

        public static TooltipGeneralSetting TooltipGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.tooltipGeneralSetting;

        public static ContextMenuGeneralSetting ContextMenuGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.contextMenuGeneralSetting;
    }
}