#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.Editor.GameEditor
{
    internal sealed class GameEditorInitializer : IEditorInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.InitComplete, OnInitComplete, this);
        }

        private static void OnInitComplete(Action onDone)
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (EditorWindow.HasOpenInstances<GameEditor>() == false)
            {
                return;
            }

            var gameEditor = EditorWindow.GetWindow<GameEditor>();

            if (gameEditor == null)
            {
                return;
            }
            
            Refresh(gameEditor);
            
            onDone();
        }

        private static async void Refresh(GameEditor gameEditor)
        {
            await UniTask.Delay(500);

            gameEditor.Repaint();
            gameEditor.ForceMenuTreeRebuild();

            await UniTask.Delay(1000);

            gameEditor.Repaint();
            gameEditor.ForceMenuTreeRebuild();
        }
    }
}

#endif