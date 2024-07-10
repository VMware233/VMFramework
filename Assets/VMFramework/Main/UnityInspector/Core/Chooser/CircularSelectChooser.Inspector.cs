#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public partial class CircularSelectChooser<TItem>
    {
        [ShowInInspector]
        private bool _pingPong => pingPong;
        
        [ShowInInspector]
        private int _startCircularIndex => startCircularIndex;

        [ShowInInspector]
        private int _currentCircularIndex => currentCircularIndex;

        [ShowInInspector]
        private int _currentCircularTimes => currentCircularTimes;

        [ShowInInspector]
        private bool _loopForward => loopForward;

        [ShowInInspector]
        private CircularSelectItem<TItem>[] _items => items;
    }
}
#endif