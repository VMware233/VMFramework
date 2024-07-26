using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Maps
{
    public class PrefabController<TPrefab> : SerializedMonoBehaviour where TPrefab : Component
    {
        [SerializeField]
        [Required]
        private GameObject initialPrefabObject;
        
        [ShowInInspector]
        public GameObject prefabObject { get; private set; }

        [ShowInInspector]
        [ReadOnly]
        public TPrefab prefab { get; private set; }
        
        private void Awake()
        {
            if (initialPrefabObject != null)
            {
                SetPrefab(initialPrefabObject);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void SetPrefab(GameObject newPrefab)
        {
            if (newPrefab == null)
            {
                Debug.LogError($"{nameof(newPrefab)} is null");
                return;
            }
            
            prefabObject = newPrefab;
            
            prefab = newPrefab.transform.QueryFirstComponentInChildren<TPrefab>(true);

            newPrefab.SetActive(true);
        }
    }
}