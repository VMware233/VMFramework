#if UNITY_EDITOR
using UnityEditor;

namespace VMFramework.Editor
{
    internal static class ResourcesFoldersCreator
    {
        [MenuItem(FoldersCreationToolsName.FOLDERS_CREATION_TOOLS_MENU_CATEGORY + "Create Resources Folders")]
        private static void CreateResourcesFolders()
        {
            var resourcesPath = IOUtility.assetsFolderPath + "/Resources";

            resourcesPath.CreateDirectory();

            var folders = new[]
            {
                "Animations", "Audios", "Fonts", "Materials", "Prefabs", "Shaders", "Images", "Models",
                "UIDocuments"
            };

            foreach (var folder in folders)
            {
                var folderPath = resourcesPath + "/" + folder;
                folderPath.CreateDirectory();
            }
            
            AssetDatabase.Refresh();
        }
    }
}
#endif