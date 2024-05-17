using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class GameEvent<TGameEvent>
    {
        private bool isPropagationStopped = false;
        
        public void StopPropagation()
        {
            isPropagationStopped = true;
        }

        protected virtual bool CanPropagate()
        {
            if (isEnabled == false)
            {
                Debug.LogWarning($"GameEvent:{id} is disabled. Cannot propagate.");
                return false;
            }
            
            return true;
        }

        [Button]
        public void Propagate()
        {
            if (CanPropagate() == false)
            {
                return;
            }
            
            isPropagationStopped = false;

            foreach (var set in callbacks.Values)
            {
                foreach (var callback in set)
                {
                    callback((TGameEvent)this);
                }
                
                if (isPropagationStopped)
                {
                    break;
                }
            }
            
            OnPropagationStopped();
        }

        protected virtual void OnPropagationStopped()
        {
            
        }
    }
}