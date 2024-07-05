using VMFramework.Procedure;
using VMFramework.ResourcesManagement;

namespace VMFramework.GameLogicArchitecture
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed class ResourcesManagementSetting
        : GlobalSetting<ResourcesManagementSetting, ResourcesManagementSettingFile>
    {
        public static ParticleGeneralSetting particleGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.particleGeneralSetting;

        public static TrailGeneralSetting trailGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.trailGeneralSetting;

        public static AudioGeneralSetting audioGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.audioGeneralSetting;
        
        public static ModelGeneralSetting modelGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.modelGeneralSetting;

        public static SpriteGeneralSetting spriteGeneralSetting =>
            globalSettingFile == null ? null : globalSettingFile.spriteGeneralSetting;
    }
}