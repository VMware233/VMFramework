#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace VMFramework.Editor
{
    internal sealed class GamePrefabViewer : OdinEditorWindow
    {
        public const string EDITOR_NAME = "Game Prefab Viewer";

        private GamePrefabViewerContainer container;

        private static GamePrefabViewer GetWindow()
        {
            bool hasOpenedWindow = HasOpenInstances<GamePrefabViewer>();
            var window = GetWindow<GamePrefabViewer>(EDITOR_NAME);

            if (hasOpenedWindow == false)
            {
                window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
            }

            return window;
        }
        
        [MenuItem("Tools/" + EDITOR_NAME)]
        public static void OpenWindow()
        {
            GetWindow();
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (container == null)
            {
                container = CreateInstance<GamePrefabViewerContainer>();
            }
            
            container.Init();
        }

        protected override object GetTarget()
        {
            return container;
        }
    }
}
#endif