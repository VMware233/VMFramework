#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using VMFramework.Core.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.Editor.BatchProcessor
{
    internal sealed class BatchProcessorContextMenuModifier : IGameEditorContextMenuModifier
    {
        void IGameEditorContextMenuModifier.ModifyContextMenu(IGameEditorContextMenuProvider provider,
            IReadOnlyList<IGameEditorMenuTreeNode> selectedNodes,GenericMenu menu)
        {
            menu.AddSeparator();

            menu.AddItem(BatchProcessorNames.BATCH_PROCESS_NAME, () =>
            {
                BatchProcessorWindow.OpenWindow(selectedNodes);
            });

            menu.AddItem(BatchProcessorNames.ADD_TO_BATCH_PROCESSOR_NAME, () =>
            {
                BatchProcessorWindow.AddToWindow(selectedNodes);
            });

            if (provider is IGlobalSettingFile)
            {
                menu.AddItem(BatchProcessorNames.ADD_ALL_GLOBAL_SETTINGS_FILE_TO_BATCH_PROCESSOR_NAME, () =>
                {
                    BatchProcessorWindow.AddToWindow(GlobalSettingFileEditorManager.GetGlobalSettings());
                });
            }
        }
    }
}
#endif