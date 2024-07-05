using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Procedure;
using Object = UnityEngine.Object;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GlobalSetting<TGlobalSetting, TGlobalSettingFile>
        : ManagerBehaviour<TGlobalSetting>, IGlobalSetting
        where TGlobalSetting : GlobalSetting<TGlobalSetting, TGlobalSettingFile>
        where TGlobalSettingFile : Object, IGlobalSettingFile
    {
        [ShowInInspector]
        public static TGlobalSettingFile globalSettingFile { get; private set; }

        public async UniTask LoadGlobalSettingFile()
        {
            var file = await GlobalSettingFileManager.LoadGlobalSettingFile<TGlobalSettingFile>();

            if (file == null)
            {
                Debug.LogError($"{typeof(TGlobalSettingFile)} Load Failed.");
                return;
            }
            
            globalSettingFile = file;
        }

        public void CheckSettings()
        {
            globalSettingFile.CheckSettings();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertGlobalSettingFileIsLoaded()
        {
            if (globalSettingFile == null)
            {
                throw new Exception($"{typeof(TGlobalSetting)} is not loaded!");
            }
        }

        IGlobalSettingFile IGlobalSetting.globalSettingFile => globalSettingFile;
    }
}