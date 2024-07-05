using Sirenix.OdinInspector;
using VMFramework.ResourcesManagement;

namespace VMFramework.GameLogicArchitecture
{
    [GlobalSettingFileConfig(FileName = nameof(ResourcesManagementSettingFile))]
    public sealed partial class ResourcesManagementSettingFile : GlobalSettingFile
    {
        public const string RESOURCES_MANAGEMENT_CATEGORY = "Resources Management";
        
        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ParticleGeneralSetting particleGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public TrailGeneralSetting trailGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public AudioGeneralSetting audioGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ModelGeneralSetting modelGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public SpriteGeneralSetting spriteGeneralSetting;
    }
}