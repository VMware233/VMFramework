using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public static class GlobalSettingCollector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGlobalSetting> Collect()
        {
            foreach (var manager in ManagerBehaviourCollector.Collect())
            {
                if (manager is IGlobalSetting globalSetting)
                {
                    yield return globalSetting;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGeneralSetting> GetAllGeneralSettings()
        {
            return Collect().SelectMany(globalSetting =>
                globalSetting.globalSettingFile.GetAllGeneralSettings());
        }
    }
}