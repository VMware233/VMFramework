using UnityEngine;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    [DisallowMultipleComponent]
    public sealed partial class ColliderMouseEventTrigger : MonoBehaviour
    {
        public bool draggable = false;

        [ShowIf(nameof(draggable))]
        public MouseButtonType dragButton = MouseButtonType.LeftButton;
        
        [field: Required]
        [field: SerializeField]
        public Transform owner { get; private set; }
        
        public void SetOwner(Transform owner)
        {
            if (this.owner != null && owner != null)
            {
                Debug.LogWarning($"ColliderMouseEventTrigger already has an owner : {owner.name}!");
            }
            
            this.owner = owner;
        }
    }
}
