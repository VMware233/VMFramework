using System;

namespace VMFramework.Core
{
    [Flags]
    public enum LeftRightDirection
    {
        None = 0,

        Left = 1,

        Right = 2,
        
        All = Left | Right
    }
}