using VMFramework.Procedure;
using VMFramework.ResourcesManagement;

namespace VMFramework.GameLogicArchitecture
{
    [ManagerCreationProvider(ManagerType.SettingCore)]
    public sealed class ResourcesManagementSetting
        : GlobalSetting<ResourcesManagementSetting, ResourcesManagementSettingFile>
    {
        public static ParticleGeneralSetting ParticleGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.particleGeneralSetting;

        public static TrailGeneralSetting TrailGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.trailGeneralSetting;

        public static AudioGeneralSetting AudioGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.audioGeneralSetting;
        
        public static ModelGeneralSetting ModelGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.modelGeneralSetting;

        public static SpriteGeneralSetting SpriteGeneralSetting =>
            GlobalSettingFile == null ? null : GlobalSettingFile.spriteGeneralSetting;
    }
}