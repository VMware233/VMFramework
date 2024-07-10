#if UNITY_EDITOR
namespace VMFramework.Editor
{
    public sealed class EditorInitializerScriptCreationViewer : InitializerScriptCreationViewer
    {
        protected override string nameSuffix => "EditorInitializer";
    }
}
#endif