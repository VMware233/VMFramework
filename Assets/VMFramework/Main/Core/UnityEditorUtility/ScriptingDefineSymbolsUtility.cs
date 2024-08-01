#if UNITY_EDITOR
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static class ScriptingDefineSymbolsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSymbol(params string[] symbols)
        {
            if (symbols.Any(symbol => symbol.IsNullOrWhiteSpace()))
            {
                Debugger.LogWarning($"Cannot add empty symbol to scripting define symbols. Symbols: {symbols.Join(", ")}");
            }
            
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            
            PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out var existingSymbols);

            var newSymbols = existingSymbols.ToList();

            newSymbols.AddIfNotContains(symbols);
            
            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, newSymbols.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveSymbol(params string[] symbols)
        {
            if (symbols.Any(symbol => symbol.IsNullOrWhiteSpace()))
            {
                Debug.LogWarning(
                    $"Cannot remove empty symbol from scripting define symbols. Symbols: {symbols.Join(", ")}");
            }

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            
            PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out var existingSymbols);
            
            var newSymbols = existingSymbols.ToList();

            newSymbols.RemoveRange(symbols);
            
            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, newSymbols.ToArray());
        }
    }
}
#endif