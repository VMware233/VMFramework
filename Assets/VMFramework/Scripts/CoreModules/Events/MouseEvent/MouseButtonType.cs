using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

namespace VMFramework.MouseEvent
{
    [Flags]
    public enum MouseButtonType {
        [LabelText("鼠标左键")]
        LeftButton = 1 << 0,
        [LabelText("鼠标右键")]
        RightButton = 1 << 1,
        [LabelText("鼠标中键")]
        MiddleButton = 1 << 2,
        [LabelText("鼠标任意键")]
        AnyButton = LeftButton | RightButton | MiddleButton,
    }
    
    public static class MouseButtonTypeUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMouseButton(this MouseButtonType mouseButtonType, int mouseButtonID)
        {
            return mouseButtonID switch
            {
                0 => mouseButtonType.HasFlag(MouseButtonType.LeftButton),
                1 => mouseButtonType.HasFlag(MouseButtonType.RightButton),
                2 => mouseButtonType.HasFlag(MouseButtonType.MiddleButton),
                _ => throw new ArgumentOutOfRangeException(nameof(mouseButtonID), mouseButtonID, null)
            };
        }
    }
}