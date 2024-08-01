using UnityEngine;

namespace VMFramework.Core
{
    public static class CommonVector2Int
    {
        /// <summary>
        /// Shorthand for Vector2Int(2, 2)
        /// </summary>
        public static readonly Vector2Int two = new(2, 2);
        
        /// <summary>
        /// Shorthand for Vector2Int(-1, 1)
        /// </summary>
        public static readonly Vector2Int upLeft = new(-1, 1);
        
        /// <summary>
        /// Shorthand for Vector2Int(1, 1)
        /// </summary>
        public static readonly Vector2Int upRight = new(1, 1);
        
        /// <summary>
        /// Shorthand for Vector2Int(-1, -1)
        /// </summary>
        public static readonly Vector2Int downLeft = new(-1, -1);
        
        /// <summary>
        /// Shorthand for Vector2Int(1, -1)
        /// </summary>
        public static readonly Vector2Int downRight = new(1, -1);
    }
}