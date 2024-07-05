#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Editor;

namespace VMFramework.Procedure.Editor
{ 
    internal static class EditorInitializer
    {
        private static readonly InitializerManager _initializerManager = new();
        
        public static IReadOnlyInitializerManager initializerManager => _initializerManager;

        public static bool isInitialized => _initializerManager.isInitialized; 
        
        [InitializeOnLoadMethod]
        private static void InitializationEntry()
        {
            EditorApplication.delayCall += Initialize;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                Initialize();
            }
        }
        
        [MenuItem(UnityMenuItemNames.EDITOR_INITIALIZATION + "Editor Initialize")]
        private static async void Initialize()
        {
            if (Application.isPlaying)
            {
                return;
            }
            
            var initializers = new List<IEditorInitializer>();
            
            foreach (var derivedClass in typeof(IEditorInitializer).GetDerivedInstantiableClasses(false))
            {
                var initializer = (IEditorInitializer)derivedClass.CreateInstance();
                
                initializers.Add(initializer);
            }
            
            _initializerManager.Set(initializers);

            await _initializerManager.Initialize();
            
            Debug.Log("Editor Initialization Done!");
        }
    }
}
#endif