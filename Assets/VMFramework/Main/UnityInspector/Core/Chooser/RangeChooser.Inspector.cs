#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public partial struct RangeChooser<TNumber>
    {
        [ShowInInspector]
        private IKCube<TNumber> _range => range;
    }
}
#endif