using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
#if UNITY_EDITOR
#endif
using VMFramework.Core;

public static class IOUtility
{
    #region Path Operations

    #region Path Combine

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string PathCombine(this string pathA, string pathB)
    {
        return Path.Combine(pathA, pathB).Replace('/', '\\');
    }

    #endregion

    #region Make Relative Path

    /// <summary>
    /// 是否可以将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath"></param>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    public static bool CanMakeRelative(string absoluteParentPath,
        string absolutePath)
    {
        absoluteParentPath.AssertIsNotNull(nameof(absoluteParentPath));
        absolutePath.AssertIsNotNull(nameof(absolutePath));

        absoluteParentPath = absoluteParentPath.Replace('\\', '/').Trim('/');
        absolutePath = absolutePath.Replace('\\', '/').Trim('/');
        return Path.GetPathRoot(absoluteParentPath).Equals(
            Path.GetPathRoot(absolutePath),
            StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// 将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath"></param>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string MakeRelative(string absoluteParentPath, string absolutePath)
    {
        absoluteParentPath = absoluteParentPath.TrimEnd('\\', '/');
        absolutePath = absolutePath.TrimEnd('\\', '/');
        string[] array = absoluteParentPath.Split('/', '\\');
        string[] array2 = absolutePath.Split('/', '\\');
        int num = -1;
        for (int i = 0;
             i < array.Length && i < array2.Length && array[i].Equals(array2[i],
                 StringComparison.CurrentCultureIgnoreCase);
             i++)
        {
            num = i;
        }

        if (num == -1)
        {
            throw new InvalidOperationException("No common directory found.");
        }

        StringBuilder stringBuilder = new();
        if (num + 1 < array.Length)
        {
            for (int j = num + 1; j < array.Length; j++)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('/');
                }

                stringBuilder.Append("..");
            }
        }

        for (int k = num + 1; k < array2.Length; k++)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append('/');
            }

            stringBuilder.Append(array2[k]);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// 尝试将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath">父绝对路径</param>
    /// <param name="absolutePath">要转换的绝对路径</param>
    /// <param name="relativePath">相对路径结果</param>
    /// <returns></returns>
    public static bool TryMakeRelative(this string absoluteParentPath,
        string absolutePath, out string relativePath)
    {
        if (CanMakeRelative(absoluteParentPath, absolutePath))
        {
            relativePath = MakeRelative(absoluteParentPath, absolutePath);
            return true;
        }

        relativePath = null;
        return false;
    }

    #endregion

    #region Try Make Path Start With

    /// <summary>
    /// 尝试将路径以startWith开头
    /// 例如：将"E:/myGame/Assets/StreamingAssets/config.json"转换为"StreamingAssets/config.json"
    /// </summary>
    /// <param name="fullPath"></param>
    /// <param name="startWith"></param>
    /// <param name="relativePath"></param>
    /// <param name="resultSeparator"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryMakeStartWith(this string fullPath, string startWith, out string relativePath, char resultSeparator)
    {
        var parts = fullPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        int startIndex = -1;
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] == startWith)
            {
                startIndex = i;
            }
        }

        if (startIndex == -1)
        {
            relativePath = null;
            return false;
        }
        
        relativePath = string.Join(resultSeparator, parts[startIndex..]);
        return true;
    }

    #endregion

    #region Get Relative Path

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetRelativePath(this string path, string relativeTo)
    {
        return Path.GetRelativePath(relativeTo, path);
    }

    #endregion

    #region Replace To

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReplaceToDirectorySeparator(this string path)
    {
        return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReplaceToAltDirectorySeparator(this string path)
    {
        return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    }

    #endregion

    #region Split Path

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] SplitPath(this string path)
    {
        return path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    }

    #endregion

    #endregion

    #region File

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ExistsFile(this string filePath)
    {
        return File.Exists(filePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OpenFile(this string filePath)
    {
        if (filePath.ExistsFile())
        {
            Process.Start(filePath);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OverWriteFile(this string filePath, string content)
    {
        FileStream fs = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);

        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);

        StreamWriter sw = new(fs, Encoding.UTF8);

        sw.Write(content);
        sw.Flush();

        sw.Close();
        fs.Close();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendFile(this string filePath, string content)
    {
        StreamWriter sw = new(filePath, true, Encoding.UTF8);

        sw.Write(content);
        sw.Flush();

        sw.Close();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReadText(this string filePath)
    {
        return File.ReadAllText(filePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetFileNameFromPath(this string path)
    {
        return Path.GetFileName(path);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetFileNameWithoutExtensionFromPath(this string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    #endregion

    #region Directory

    /// <summary>
    /// 目录是否存在
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ExistsDirectory(this string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    /// <summary>
    /// 获取目录路径
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetDirectoryPath(this string filePath)
    {
        return Path.GetDirectoryName(filePath);
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns>
    /// true:目录不存在，创建成功
    /// false:目录已存在
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CreateDirectory(this string directoryPath)
    {
        var existed = Directory.Exists(directoryPath);
        if (existed == false)
        {
            Directory.CreateDirectory(directoryPath);
        }

        return existed == false;
    }

    /// <summary>
    /// 在资源管理器中打开目录
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="createIfNotExisted"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OpenDirectory(this string directoryPath, bool createIfNotExisted)
    {
        directoryPath = directoryPath.Replace('/', '\\');
        if (createIfNotExisted)
        {
            CreateDirectory(directoryPath);
        }
        Process.Start("explorer.exe", directoryPath);
    }

    /// <summary>
    /// 获取目录下的所有文件路径
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> GetAllFilesPath(this string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
        }

        return Directory.EnumerateFiles(directoryPath, "*.*",
            SearchOption.TopDirectoryOnly);
    }

    #endregion
}
