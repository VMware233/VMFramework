using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Configuration
{
    public interface IBaseConfigClass
    {
        public bool initDone { get; }
        
        protected void OnInspectorInit()
        {
            
        }
        
        public void CheckSettings()
        {

        }
    }
    
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [JsonObject(MemberSerialization.OptIn, ItemTypeNameHandling = TypeNameHandling.All)]
    [OnInspectorInit("@((BaseConfigClass)$value)?.OnInspectorInit()")]
    public abstract class BaseConfigClass : IBaseConfigClass
    {
        public bool initDone { get; private set; } = false;

        protected virtual void OnInspectorInit()
        {
            
        }

        public virtual void CheckSettings()
        {

        }

        public virtual void Init()
        {
            Debug.Log($"开始加载{this}");
            OnInit();

            initDone = true;
        }

        protected virtual void OnInit()
        {

        }

        #region Interface Implementation

        void IBaseConfigClass.OnInspectorInit()
        {
            OnInspectorInit();
        }

        #endregion
    }
}
