

using System;
using System.Collections.Generic;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GeneralSetting : GameSettingBase, IGeneralSetting
    {
        protected const string DATA_STORAGE_CATEGORY = "Data Storage";
        
        protected const string JSON_CATEGORY = "JSON";
        
        protected const string LOCALIZABLE_SETTING_CATEGORY = "Localization";

        protected virtual IEnumerable<InitializationAction> GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInitInternal, this);
        }
        
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            return GetInitializationActions();
        }

        private void OnInitInternal(Action onDone)
        {
            OnInit();
            onDone();
        }

        protected virtual void OnInit()
        {
            
        }
    }
}
