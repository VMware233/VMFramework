using VMFramework.GameEvents;

namespace VMFramework.Examples
{
    public class UIOpenEvent : PooledGameEvent<UIOpenEvent>
    {
        public string uiID { get; private set; }
        
        public void SetParameters(string uiID)
        {
            this.uiID = uiID;
        }

        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();

            uiID = null;
        }
    }
}