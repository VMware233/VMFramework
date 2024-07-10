#if UNITY_EDITOR
using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Get Asset By Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object GetAssetByPath(this string path)
        {
            var result = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAssetByPath<T>(this string path) where T : Object
        {
            var result = AssetDatabase.LoadAssetAtPath(path, typeof(T));
            
            return (T) result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object GetAssetByPath(this string path, Type type)
        {
            var result = AssetDatabase.LoadAssetAtPath(path, type);
            
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetByPath(this string path, Type type, out Object result)
        {
            result = AssetDatabase.LoadAssetAtPath(path, type);

            return result != null;
        }

        #endregion
        
        #region Get Asset Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetPath(this Object obj, out string assetPath)
        {
            if (obj.IsAsset() == false)
            {
                assetPath = null;
                return false;
            }
            
            assetPath = obj.GetAssetPath();
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetPathWithWarning(this Object obj, out string assetPath)
        {
            if (obj.IsAssetWithWarning() == false)
            {
                assetPath = null;
                return false;
            }
            
            assetPath = obj.GetAssetPath();
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetAbsolutePath(this Object obj, out string absolutePath)
        {
            if (obj.IsAsset() == false)
            {
                absolutePath = null;
                return false;
            }
            
            absolutePath = obj.GetAssetAbsolutePath();
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetAbsolutePathWithWarning(this Object obj, out string absolutePath)
        {
            if (obj.IsAssetWithWarning() == false)
            {
                absolutePath = null;
                return false;
            }
            
            absolutePath = obj.GetAssetAbsolutePath();
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAssetPath(this Object obj)
        {
            return AssetDatabase.GetAssetPath(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GUIDToAssetPath(this string guid)
        {
            return AssetDatabase.GUIDToAssetPath(guid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAssetAbsolutePath(this Object asset)
        {
            return asset == null
                ? string.Empty
                : CommonFolders.projectFolderPath.PathCombine(asset.GetAssetPath()).ReplaceToDirectorySeparator();
        }

        #endregion

        #region Rename

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Rename(this Object obj, string newName)
        {
            if (newName.IsNullOrEmptyAfterTrim())
            {
                Debug.LogWarning($"{obj.name}'s New Name cannot be Null or Empty.");
                return;
            }
            
            string selectedAssetPath = AssetDatabase.GetAssetPath(obj);

            if (selectedAssetPath.IsNullOrEmpty())
            {
                obj.name = newName;
            }
            else
            {
                AssetDatabase.RenameAsset(selectedAssetPath, newName);
            }

            Undo.RecordObject(obj, "Rename");
            
            obj.EnforceSave();
        }

        #endregion
    }
}
#endif