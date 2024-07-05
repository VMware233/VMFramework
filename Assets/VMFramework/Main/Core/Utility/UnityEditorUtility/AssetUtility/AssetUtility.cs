#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Save

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnforceSave(this Object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("保存对象为空，无法保存");
                return;
            }
            
            obj.SetEditorDirty();

            if (EditorApplication.isUpdating == false)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEditorDirty(this Object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("保存对象为空，无法设置Dirty");
                return;
            }
            
            UnityEditor.EditorUtility.SetDirty(obj);
        }

        #endregion

        #region Copy Asset

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CopyAssetTo<T>(this T obj, string newPath) where T : Object
        {
            if (obj == null)
            {
                Debug.LogWarning("复制对象为空，无法复制");
                return null;
            }

            var sourcePath = obj.GetAssetPath();

            if (sourcePath.IsNullOrEmpty())
            {
                Debug.LogWarning("复制对象路径为空，无法复制");
                return null;
            }

            if (newPath.IsNullOrEmpty())
            {
                Debug.LogWarning("复制目标路径为空，无法复制");
                return null;
            }

            var absoluteDirectoryPath = newPath.ConvertAssetPathToAbsolutePath().GetDirectoryPath();

            if (absoluteDirectoryPath.ExistsDirectory() == false)
            {
                Debug.LogWarning($"复制目标路径{absoluteDirectoryPath}不存在，无法复制");
                return null;
            }

            if (AssetDatabase.CopyAsset(sourcePath, newPath) == false)
            {
                Debug.LogWarning($"复制{obj.GetType()}从{sourcePath}到{newPath}失败");
                return null;
            }
            
            var newObj = AssetDatabase.LoadAssetAtPath<T>(newPath);
            
            return newObj;
        }

        #endregion

        #region Exists Asset

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ExistsAsset(this string path)
        {
            path = path.MakeAssetPath();
            
            var absolutePath = path.ConvertAssetPathToAbsolutePath();
            
            return absolutePath.ExistsFile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ExistsAssetWithWarning(this string path)
        {
            if (path.ExistsAsset())
            {
                Debug.LogWarning($"The asset already exists at {path}");
                return true;
            }
            
            return false;
        }

        #endregion

        #region Move Asset

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveAssetToNewFolder(this Object obj, string newFolder)
        {
            if (obj == null)
            {
                Debug.LogWarning($"Object is null, cannot move to {newFolder}");
                return;
            }
            
            var sourcePath = obj.GetAssetPath();
            
            if (sourcePath.IsNullOrEmpty())
            {
                Debug.LogWarning($"Object path is null, cannot move to {newFolder}");
                return;
            }
            
            if (newFolder.IsNullOrEmpty())
            {
                Debug.LogWarning($"New folder path is null or empty, cannot move to {newFolder}");
                return;
            }

            newFolder = newFolder.MakeAssetPath();
            
            if (newFolder.IsAssetPath() == false)
            {
                Debug.LogWarning($"New folder path {newFolder} is not an asset path, cannot move!");
                return;
            }
            
            var absoluteDirectoryPath = newFolder.ConvertAssetPathToAbsolutePath();
            absoluteDirectoryPath.CreateDirectory();

            var fileName = sourcePath.GetFileNameFromPath();
            var newPath = newFolder.PathCombine(fileName);
            
            AssetDatabase.MoveAsset(sourcePath, newPath);
            
            obj.EnforceSave();
        }

        #endregion

        #region Create Asset

        public static bool TryCreateAsset(this Object obj, string path)
        {
            if (CommonFolders.projectFolderPath.TryMakeRelative(path, out path) == false)
            {
                Debug.LogWarning($"保存路径{path}不在Assets文件夹下，创建{obj.GetType()}失败");

                Object.DestroyImmediate(obj);
                return false;
            }
            
            obj.CreateAsset(path);
            return true;
        }
        
        public static void CreateAsset(this Object obj, string path)
        {
            AssetDatabase.CreateAsset(obj, path);
            obj.EnforceSave();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Delete Asset
        
        public static void DeleteAsset(this Object obj)
        {
            if (obj.IsAsset() == false)
            {
                Debug.LogWarning($"{obj}不是Asset，无法删除");
                return;
            }
            
            AssetDatabase.DeleteAsset(obj.GetAssetPath());
        }

        #endregion

        #region Is Asset
        
        public static bool IsAsset(this Object obj)
        {
            return AssetDatabase.Contains(obj);
        }

        #endregion

        #region Get Asset GUID

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAssetGUID(this Object obj)
        {
            return AssetDatabase.AssetPathToGUID(obj.GetAssetPath());
        }

        #endregion
    }
}

#endif