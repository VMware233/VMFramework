using UnityEngine;

namespace VMFramework.TestBenches
{
    public abstract class AngleTestBench : MonoBehaviour
    {
        [Range(0, 10f)]
        public float length = 3f;
        
        public Vector3 origin => transform.position;
    }
}