#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;
using VMFramework.Core.Linq;

namespace VMFramework.Core.Editor
{
    public static class EditorUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonoScript MonoScriptFromScriptName(string scriptName)
        {
            foreach (var monoScript in scriptName.FindAssetsOfName<MonoScript>(null))
            {
                return monoScript;
            }

            return null;
            // return AssetDatabase.FindAssets($"{scriptName} t:MonoScript")
            //     .Select(AssetDatabase.GUIDToAssetPath)
            //     .Select(AssetDatabase.LoadAssetAtPath<MonoScript>)
            //     .FirstOrDefault();
        }

        public static bool OpenScriptOfType(this Type type)
        {
            if (type == null)
            {
                return false;
            }
            
            var typeName = type.Name;
            if (type.IsGenericType)
            {
                type = type.GetGenericTypeDefinition();
                typeName = typeName[..typeName.IndexOf('`')];
            }
            
            var mono = MonoScriptFromScriptName(typeName);
            if (mono != null)
            {
                AssetDatabase.OpenAsset(mono);
                return true;
            }

            Debug.LogWarning($"打开Class:{type}失败，因为同名脚本文件{typeName}.cs不存在");
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OpenScriptOfObject(this object obj)
        {
            obj.GetType().OpenScriptOfType();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OpenScriptOfObjects<TObject>(this IEnumerable<TObject> objects)
        {
            objects.Examine(obj => obj.OpenScriptOfObject());
        }
    }
}

#endif