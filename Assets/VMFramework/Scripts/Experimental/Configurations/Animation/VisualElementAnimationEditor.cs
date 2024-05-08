//using Basis.Configuration;
//using Sirenix.Utilities;
//using Sirenix.Utilities.Editor;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class VisualElementAnimationEditor : EditorWindow
//{
//    protected const string EDITOR_NAME = "UIToolkit动画编辑器";

//    [SerializeField]
//    private VisualTreeAsset m_VisualTreeAsset = default;

//    public static void OpenAnimation(VisualElementAnimation visualElementAnimation)
//    {
//        VisualElementAnimationEditor window =
//            GetWindow<VisualElementAnimationEditor>();
//        window.titleContent = new GUIContent(EDITOR_NAME);
//        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
//    }

//    public void Open(VisualElementAnimation visualElementAnimation)
//    {

//    }

//    public void CreateGUI()
//    {
//        // Each editor window contains a root VisualElement object
//        VisualElement root = rootVisualElement;

//        // VisualElements objects can contain other VisualElement following a tree hierarchy.
//        VisualElement label = new Label("Hello World! From C#");
//        root.Add(label);

//        // Instantiate UXML
//        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
//        root.Add(labelFromUXML);
//    }
//}
