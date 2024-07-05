using Cysharp.Threading.Tasks;
using VMFramework.Configuration;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGlobalSetting : ICheckableConfig
    {
        public IGlobalSettingFile globalSettingFile { get; }
        
        public UniTask LoadGlobalSettingFile();
    }
}