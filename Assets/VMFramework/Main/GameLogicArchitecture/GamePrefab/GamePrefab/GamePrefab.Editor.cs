#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [OnInspectorInit("@((IInspectorConfig)$value)?.OnInspectorInit()")]
    public partial class GamePrefab
    {
        #region On Inspector Init

        protected virtual void OnInspectorInit()
        {
            
        }
        
        void IInspectorConfig.OnInspectorInit()
        {
            OnInspectorInit();
        }

        #endregion
        
        #region ID

        private string GetIDPlaceholderText()
        {
            const string placeholderText = "请输入ID";
            if (idSuffix.IsNullOrEmptyAfterTrim())
            {
                return placeholderText;
            }

            return placeholderText + $"并以_{idSuffix}结尾";
        }

        #endregion
    }
}
#endif