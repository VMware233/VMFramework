#if UNITY_EDITOR
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace VMFramework.Core.Editor
{
    public static class SceneUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetBuildSceneNames()
        {
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled == false)
                {
                    continue;
                }
                
                string scenePath = scene.path;
                string sceneName = scenePath.GetFileNameWithoutExtensionFromPath();
                yield return sceneName;
            }
        }
    }
}
#endif