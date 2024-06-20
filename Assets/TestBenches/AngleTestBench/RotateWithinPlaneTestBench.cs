using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.TestBenches
{
    public sealed class RotateWithinPlaneTestBench : AngleTestBench
    {
        public Vector3 vector;
        
        public Vector3 nearVector;
        
        [Range(0f, 360f)]
        public float angle;

        private void OnDrawGizmos()
        {
            if (enabled == false) return;
            
            Gizmos.color = Color.red;
            GizmosUtility.DrawLine(origin, vector, length);
            GizmosUtility.DrawLine(origin, nearVector, length);
            Gizmos.color = Color.green;
            var result = vector.RotateWithinPlane(nearVector, angle);
            GizmosUtility.DrawLine(origin, result, length);
        }
    }
}