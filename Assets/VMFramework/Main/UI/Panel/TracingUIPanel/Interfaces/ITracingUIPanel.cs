using UnityEngine;

namespace VMFramework.UI
{
    public interface ITracingUIPanel : IUIPanelController
    {
        public bool TryUpdatePosition(Vector2 screenPosition);

        public void SetPivot(Vector2 pivot);
    }
}
