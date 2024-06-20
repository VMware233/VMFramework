using System;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.TestBenches
{
    public sealed class Rotate3DTestBench : AngleTestBench
    {
        public Vector3 axis;
        
        public RotateDirection rotateDirection;
        
        public Vector3 direction;
        
        [Range(0f, 360f)]
        public float angle;

        private void OnDrawGizmos()
        {
            if (enabled == false) return;
            
            Gizmos.color = Color.red;
            GizmosUtility.DrawLine(origin, direction, length);
            GizmosUtility.DrawLine(origin, axis, length);
            Gizmos.color = Color.green;
            var result = direction.Rotate(angle, axis, rotateDirection);
            GizmosUtility.DrawLine(origin, result, length);
        }
    }
}