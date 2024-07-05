#if UNITY_EDITOR
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Convert Asset Path To Absolute Path

        /// <summary>
        /// AssetPath starts with "Assets/"
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ConvertAssetPathToAbsolutePath(this string assetPath)
        {
            return CommonFolders.projectFolderPath.PathCombine(assetPath);
        }

        #endregion

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
                : CommonFolders.projectFolderPath.PathCombine(asset.GetAssetPath())
                    .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        #endregion

        #region Is Asset Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAssetPath(this string path)
        {
            if (path == "Assets")
            {
                return true;
            }

            if (path.StartsWith("Assets" + Path.DirectorySeparatorChar) ||
                path.StartsWith("Assets" + Path.AltDirectorySeparatorChar))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Make Asset Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MakeAssetPath(this string path)
        {
            return path.ReplaceToAltDirectorySeparator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MakeAssetPath(this string path, string extension)
        {
            path = path.ReplaceToAltDirectorySeparator();

            if (extension.StartsWith(".") == false)
            {
                extension = "." + extension;
            }

            if (path.EndsWith(extension) == false)
            {
                path += extension;
            }
            
            return path;
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