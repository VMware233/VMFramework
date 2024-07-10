#if UNITY_EDITOR
using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Editor
{
    public static class GlobalSettingFileFolderTypeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToFolderString(this GlobalSettingFileFolderType folderType)
        {
            return folderType switch
            {
                GlobalSettingFileFolderType.DefaultFolder => "ConfigurationPath.DEFAULT_GLOBAL_SETTINGS_PATH",
                GlobalSettingFileFolderType.InternalFolder => "ConfigurationPath.INTERNAL_GLOBAL_SETTINGS_PATH",
                GlobalSettingFileFolderType.Custom => "/*Enter Folder Path Here*/",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
#endif