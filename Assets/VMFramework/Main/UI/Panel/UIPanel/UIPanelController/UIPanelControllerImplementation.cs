using System;
using Sirenix.OdinInspector;

namespace VMFramework.UI
{
    public partial class UIPanelController : IUIPanelController
    {
        public string id => Preset.id;

        public bool isUnique => Preset.isUnique;
        
        [ShowInInspector]
        public bool uiEnabled { get; private set; }
        
        [ShowInInspector]
        public bool IsOpening { get; private set; }

        [ShowInInspector]
        public bool IsClosing { get; private set; }

        [ShowInInspector]
        public bool isOpened { get; private set; }

        [ShowInInspector]
        public IUIPanelController SourceUIPanel { get; private set; }

        IUIPanelController IUIPanelController.SourceUIPanel
        {
            get => SourceUIPanel;
            set => SourceUIPanel = value;
        }
        
        public abstract event Action<IUIPanelController> OnOpenEvent;
        
        public abstract event Action<IUIPanelController> OnCloseEvent;

        public event Action<IUIPanelController> OnOpenInstantlyEvent;

        public event Action<IUIPanelController> OnCloseInstantlyEvent;
        
        public event Action<IUIPanelController> OnDestructEvent;

        public event Action<IUIPanelController> OnCrashEvent;

        [ShowInInspector]
        bool IUIPanelController.isOpening
        {
            get => IsOpening;
            set => IsOpening = value;
        }
        
        [ShowInInspector]
        bool IUIPanelController.isClosing
        {
            get => IsClosing;
            set => IsClosing = value;
        }

        void IUIPanelController.OnRecreate(IUIPanelController newPanel)
        {
            OnRecreate(newPanel);
        }

        void IUIPanelController.OnCreate()
        {
            OnCreate();
        }

        void IUIPanelController.Crash()
        {
            OnCrashEvent?.Invoke(this);

            OnCrash();
            
            if (isUnique)
            {
                UIPanelManager.RecreateUniquePanel(Preset.id);
            }
            else
            {
                this.Destruct();
            }
        }

        void IUIPanelController.Destruct()
        {
            OnDestruction();
            OnDestructEvent?.Invoke(this);
            Destroy(gameObject);
        }
        
        void IUIPanelController.PreOpen(IUIPanelController source)
        {
            
        }

        void IUIPanelController.PostClose(IUIPanelController source)
        {
            
        }
    }
}