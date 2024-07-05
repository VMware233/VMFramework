#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal sealed class GlobalSettingEditorInitializer : IEditorInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.BeforeInitStart, OnBeforeInitStart, this);
            yield return new(InitializationOrder.InitStart, OnInitStart, this);
        }

        private static void OnBeforeInitStart(Action onDone)
        {
            GlobalSettingFileEditorManager.CheckGlobalSettingsFile();
            
            onDone();
        }

        private static async void OnInitStart(Action onDone)
        {
            var tasks = new List<UniTask>();
            
            foreach (var manager in ManagerCreator.managers)
            {
                if (manager is IGlobalSetting globalSetting)
                {
                    tasks.Add(globalSetting.LoadGlobalSettingFileInEditor());
                }
            }

            await UniTask.WhenAll(tasks);

            foreach (var globalSettingFile in GlobalSettingFileEditorManager.GetGlobalSettings())
            {
                globalSettingFile.AutoFindSettings();
            }
            
            onDone();
        }
    }
}
#endif