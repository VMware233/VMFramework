#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GlobalSettingFileEditorManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MenuItem(UnityMenuItemNames.GLOBAL_SETTINGS + "Check Global Settings File")]
        public static void CheckGlobalSettingsFile()
        {
            AssetDatabase.Refresh();
            
            foreach (var (type, path, fileName) in GetGlobalSettingsPaths())
            {
                var asset = type.FindOrCreateScriptableObjectAtPath(path, out var isNewlyCreated);

                if (isNewlyCreated)
                {
                    Debug.Log($"Created new {type.Name} at path {path}.", asset);
                    
                    var entry = asset.CreateOrMoveEntryToDefaultGroup();
                    
                    entry.SetAddress(fileName.GetFileNameWithoutExtensionFromPath());
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGlobalSettingFile> GetGlobalSettings()
        {
            foreach (var (type, path, fileName) in GetGlobalSettingsPaths())
            {
                var asset = path.GetAssetByPath(type);

                if (asset is IGlobalSettingFile settingFile)
                {
                    yield return settingFile;
                }
                else
                {
                    Debug.LogWarning($"{nameof(IGlobalSettingFile)} at path {path} is not of type {type}.");
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGlobalSettingPath(this IGlobalSettingFile globalSettingFile, out string path,
            out string fileName)
        {
            return TryGetGlobalSettingPath(globalSettingFile.GetType(), out path, out fileName);
        }

        public static bool TryGetGlobalSettingPath(Type globalSettingFileType, out string path, out string fileName)
        {
            if (GlobalSettingFileManager.typeConfigs.TryGetValue(globalSettingFileType, out var config) ==
                false)
            {
                Debug.LogWarning($"Cannot find config for type {globalSettingFileType}.");
                
                path = null;
                fileName = null;
                
                return false;
            }
            
            if (globalSettingFileType.TryGetAttribute(false, out GlobalSettingFileEditorConfigAttribute editorConfig) ==
                false)
            {
                Debug.LogWarning($"Type {globalSettingFileType.Name} does not have a " +
                                 $"{nameof(GlobalSettingFileEditorConfigAttribute)} attribute.");
                
                path = null;
                fileName = null;
                
                return false;
            }

            var directoryPath = GetAssetFolderPath(editorConfig.FolderPath);
                
            fileName = config.FileName;

            if (fileName.EndsWith(".asset") == false)
            {
                fileName += ".asset";
            }
            
            path = directoryPath.PathCombine(fileName);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<(Type type, string path, string fileName)> GetGlobalSettingsPaths()
        {
            foreach (var type in GlobalSettingFileManager.typeConfigs.Keys)
            {
                if (TryGetGlobalSettingPath(type, out var directoryPath, out var fileName) == false)
                {
                    continue;
                }
                
                yield return (type, directoryPath, fileName);
            }
        }

        private static string GetAssetFolderPath(string folderPath)
        {
            if (folderPath.IsAssetPath())
            {
                return folderPath;
            }

            var folderParts = folderPath.SplitPath();

            var firstFolderName = folderParts[0];

            foreach (var directoryFullPath in Directory.EnumerateDirectories(CommonFolders.assetsFolderPath, "*",
                         SearchOption.AllDirectories))
            {
                var directoryName = directoryFullPath.GetFileNameFromPath();

                if (directoryName != firstFolderName)
                {
                    continue;
                }

                folderPath = directoryFullPath.GetDirectoryPath().PathCombine(folderPath);

                folderPath = folderPath.GetRelativePath(CommonFolders.projectFolderPath).MakeAssetPath();
            }

            return folderPath;
        }
    }
}
#endif