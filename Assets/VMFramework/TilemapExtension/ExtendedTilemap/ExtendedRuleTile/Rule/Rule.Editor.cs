#if UNITY_EDITOR
using VMFramework.Core;

namespace VMFramework.ExtendedTilemap
{
    public partial class Rule
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            layers ??= new();

            upperLeft ??= new();
            upper ??= new();
            upperRight ??= new();
            left ??= new();
            right ??= new();
            lowerLeft ??= new();
            lower ??= new();
            lowerRight ??= new();

            if (center.IsNullOrEmpty())
            {
                center = "";
            }
        }
    }
}
#endif