using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class UISetting : GlobalSetting<UISetting, UISettingFile>
    {
        public static UIPanelGeneralSetting uiPanelGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.uiPanelGeneralSetting;

        public static UIPanelProcedureGeneralSetting uiPanelProcedureGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.uiPanelProcedureGeneralSetting;

        public static DebugUIPanelGeneralSetting debugUIPanelGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.debugUIPanelGeneralSetting;

        public static TooltipGeneralSetting tooltipGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.tooltipGeneralSetting;

        public static ContextMenuGeneralSetting contextMenuGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.contextMenuGeneralSetting;
    }
}