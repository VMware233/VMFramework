using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Procedure 
{
    public sealed partial class DefaultGlobalScenesGeneralSetting : GeneralSetting
    {
        [BuildSceneName]
        public List<string> sceneNames = new();
    }
}