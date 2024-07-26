using System;

namespace VMFramework.Core
{
    [Flags]
    public enum SixTypesDirection
    {
        None = 0,

        Left = 1,

        Right = 2,

        Up = 4,

        Down = 8,

        Forward = 16,

        Back = 32,

        All = Left | Right | Up | Down | Forward | Back
    }
}