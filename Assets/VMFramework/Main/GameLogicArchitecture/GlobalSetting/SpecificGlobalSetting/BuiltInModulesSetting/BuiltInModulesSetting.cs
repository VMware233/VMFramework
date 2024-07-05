using VMFramework.Containers;
using VMFramework.Procedure;
using VMFramework.Properties;
using VMFramework.Recipes;

namespace VMFramework.GameLogicArchitecture
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed partial class BuiltInModulesSetting
        : GlobalSetting<BuiltInModulesSetting, BuiltInModulesSettingFile>
    {
        public static GamePropertyGeneralSetting gamePropertyGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.gamePropertyGeneralSetting;

        public static TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.tooltipPropertyGeneralSetting;

        public static CameraGeneralSetting cameraGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.cameraGeneralSetting;

        public static ContainerGeneralSetting containerGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.containerGeneralSetting;

        public static RecipeGeneralSetting recipeGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.recipeGeneralSetting;
    }
}