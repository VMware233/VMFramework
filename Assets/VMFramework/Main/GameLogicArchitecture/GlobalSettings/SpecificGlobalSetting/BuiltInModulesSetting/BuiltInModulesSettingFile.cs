using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using VMFramework.Containers;
using VMFramework.Properties;
using VMFramework.Recipes;

namespace VMFramework.GameLogicArchitecture
{
    [GlobalSettingFileConfig(FileName = nameof(BuiltInModulesSettingFile))]
    public sealed partial class BuiltInModulesSettingFile : GlobalSettingFile
    {
        public const string BUILTIN_MODULE_CATEGORY = "Builtin Modules";
        
        [FormerlySerializedAs("propertyGeneralSetting")]
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public GamePropertyGeneralSetting gamePropertyGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public CameraGeneralSetting cameraGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ContainerGeneralSetting containerGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public RecipeGeneralSetting recipeGeneralSetting;
    }
}