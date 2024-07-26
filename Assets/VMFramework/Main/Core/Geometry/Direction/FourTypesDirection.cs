using System;

namespace VMFramework.Core
{
    [Flags]
    public enum FourTypesDirection
    {
        None = 0,

        Left = 1,
        
        Right = 2,
        
        Up = 4,
        
        Down = 8,
        
        Horizontal = Left | Right,
        
        Vertical = Up | Down,

        All = Up | Down | Left | Right
    }
}