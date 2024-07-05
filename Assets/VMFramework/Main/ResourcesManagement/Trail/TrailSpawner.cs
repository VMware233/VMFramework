using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Configuration;
using VMFramework.Core;
using UnityEngine;
using Newtonsoft.Json;
using VMFramework.Core.Pool;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public class TrailSpawnConfig : BaseConfig
    {
        [GamePrefabID(typeof(TrailPreset))]
        [IsNotNullOrEmpty]
        [SerializeField]
        protected string trailID;

        [SerializeField, JsonProperty]
        protected IChooserConfig<Vector3> position = new SingleValueChooserConfig<Vector3>();

        public TrailRenderer GenerateTrail(Transform parent)
        {
            var trailRenderer = TrailSpawner.Spawn(trailID, position.GetValue(), parent);

            return trailRenderer;
        }
    }

    public static class TrailSpawner
    {
        private static readonly Dictionary<string, IComponentPool<TrailRenderer>> allPools = new();
        private static readonly Dictionary<TrailRenderer, string> allTrailIDs = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnTrailToDefaultContainer(TrailRenderer trail)
        {
            trail.transform.SetParent(ResourcesManagementSetting.trailGeneralSetting.container);
        }

        /// <summary>
        /// Returns a TrailRenderer to the pool of the corresponding ID.
        /// </summary>
        /// <param name="trail"></param>
        public static void Return(TrailRenderer trail)
        {
            if (trail.gameObject.activeSelf)
            {
                var id = allTrailIDs[trail];
                var pool = allPools[id];

                trail.transform.SetParent(ResourcesManagementSetting.trailGeneralSetting.container);
                pool.Return(trail);
            }
        }

        /// <summary>
        /// Spawns a new trail with the given ID and position.
        /// If the parent Transform is Null, the position parameter is treated as world space position,
        /// otherwise it is treated as local position.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static TrailRenderer Spawn(string id, Vector3 pos, Transform parent = null)
        {
            var registeredTrail = GamePrefabManager.GetGamePrefabStrictly<TrailPreset>(id);

            if (allPools.TryGetValue(id, out var pool) == false)
            {
                pool = new StackComponentPool<TrailRenderer>(() =>
                {
                    var registeredTrail = GamePrefabManager.GetGamePrefabStrictly<TrailPreset>(id);
                    var prefab = registeredTrail.trailPrefab;
                    var newTrail = Object.Instantiate(prefab,
                        ResourcesManagementSetting.trailGeneralSetting.container);
                    return newTrail;
                });
                allPools[id] = pool;
            }

            var newTrail = pool.Get(parent);

            allTrailIDs[newTrail] = id;

            if (parent == null)
            {
                newTrail.transform.position = pos;
            }
            else
            {
                newTrail.transform.localPosition = pos;
            }

            1.DelayFrameAction(() => { newTrail.Clear(); });

            return newTrail;
        }
    }
}