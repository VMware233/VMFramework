#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Editor
{
    public static class AssetPathUtility
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
            return CommonFolders.projectFolderPath.PathCombine(assetPath).ReplaceToDirectorySeparator();
        }

        #endregion

        #region Convert Relative Asset Path To Asset Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAbsoluteFolderPathByRelativeFolderPath(this string relativeFolderPath,
            bool completeMatch, out string assetFolderPath)
        {
            foreach (var result in relativeFolderPath.GetAbsoluteFolderPathsByRelativeFolderPath(completeMatch))
            {
                assetFolderPath = result;
                return true;
            }

            assetFolderPath = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAbsoluteFolderPathsByRelativeFolderPath(this string relativeFolderPath,
            bool completeMatch)
        {
            if (relativeFolderPath.IsAssetPath())
            {
                yield return relativeFolderPath.ConvertAssetPathToAbsolutePath();
                yield break;
            }

            if (completeMatch == false)
            {
                foreach (var (directoryAbsolutePath, _) in CommonFolders.assetsFolderPath.EnumerateAllDirectories()
                             .PathEndsWith(relativeFolderPath, true))
                {
                    yield return directoryAbsolutePath;
                }
            }
            else
            {
                foreach (var directoryAbsolutePath in CommonFolders.assetsFolderPath.EnumerateAllDirectories())
                {
                    if (directoryAbsolutePath.PathEndsWith(relativeFolderPath))
                    {
                        yield return directoryAbsolutePath;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAssetFolderPathByRelativeFolderPath(this string relativeAssetFolderPath,
            bool completeMatch, out string assetFolderPath)
        {
            foreach (var result in relativeAssetFolderPath.GetAssetFolderPathsByRelativeFolderPath(completeMatch))
            {
                assetFolderPath = result;
                return true;
            }

            assetFolderPath = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAssetFolderPathsByRelativeFolderPath(this string relativeAssetFolderPath,
            bool completeMatch)
        {
            if (relativeAssetFolderPath.IsAssetPath())
            {
                yield return relativeAssetFolderPath;
                yield break;
            }

            if (completeMatch == false)
            {
                foreach (var (directoryAbsolutePath, matchCount) in CommonFolders.assetsFolderPath
                             .EnumerateAllDirectories().PathEndsWith(relativeAssetFolderPath, true))
                {
                    var assetFolderPath = directoryAbsolutePath.PathTrimEnd(matchCount);
                    assetFolderPath = assetFolderPath.PathCombine(relativeAssetFolderPath);
                    assetFolderPath = assetFolderPath.GetRelativePath(CommonFolders.projectFolderPath);
                    assetFolderPath = assetFolderPath.MakeAssetPath();
                    yield return assetFolderPath;
                }
            }
            else
            {
                foreach (var directoryFullPath in CommonFolders.assetsFolderPath.EnumerateAllDirectories())
                {
                    if (directoryFullPath.PathEndsWith(relativeAssetFolderPath))
                    {
                        var assetFolderPath = directoryFullPath.GetRelativePath(CommonFolders.projectFolderPath);
                        assetFolderPath = assetFolderPath.MakeAssetPath();
                        yield return assetFolderPath;
                    }
                }
            }
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
    }
}
#endif