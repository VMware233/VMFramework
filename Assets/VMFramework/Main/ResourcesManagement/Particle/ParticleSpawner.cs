using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Pool;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class ParticleSpawner : SerializedMonoBehaviour
    {
        private static readonly Dictionary<string, IComponentPool<ParticleSystem>> allPools = new();

        private static readonly Dictionary<ParticleSystem, string> allParticleIDs = new();

        private static IComponentPool<ParticleSystem> CreatePool(string id)
        {
            return new StackComponentPool<ParticleSystem>(() =>
            {
                var registeredParticle = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);
                var prefab = registeredParticle.particlePrefab;
                var particleSystem = Instantiate(prefab,
                    ResourcesManagementSetting.particleGeneralSetting.container);
                return particleSystem;
            }, onReturnCallback: particle =>
            {
                particle.SetActive(false);
                particle.transform.SetParent(ResourcesManagementSetting.particleGeneralSetting.container);
            });
        }

        /// <summary>
        /// Returns a particle to the pool.
        /// </summary>
        /// <param name="particle"></param>
        public static void Return(ParticleSystem particle)
        {
            if (particle == null)
            {
                return;
            }

            if (particle.gameObject.activeSelf)
            {
                var id = allParticleIDs[particle];
                var pool = allPools[id];

                pool.Return(particle);
            }
        }

        /// <summary>
        /// Spawns a particle with the given ID at the given position.
        /// If the parent Transform is null, the position is treated as world space position, 
        /// otherwise it is treated as local position.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <param name="parent"></param>
        /// <param name="isWorldSpace"></param>
        /// <returns></returns>
        [Button]
        public static ParticleSystem Spawn([GamePrefabID(typeof(ParticlePreset))] string id, Vector3 pos,
            Transform parent = null, bool isWorldSpace = true)
        {
            var registeredParticle = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);

            if (allPools.TryGetValue(id, out var pool) == false)
            {
                pool = CreatePool(id);
                allPools[id] = pool;
            }

            var newParticleSystem = pool.Get(parent);

            allParticleIDs[newParticleSystem] = id;

            if (isWorldSpace)
            {
                newParticleSystem.transform.position = pos;
            }
            else
            {
                newParticleSystem.transform.localPosition = pos;
            }

            newParticleSystem.Clear();
            newParticleSystem.Stop();

            1.DelayFrameAction(() => { newParticleSystem.Play(); });

            if (registeredParticle.enableDurationLimitation)
            {
                registeredParticle.duration.GetValue().DelayAction(() => { Return(newParticleSystem); });
            }

            return newParticleSystem;
        }

        [Button]
        public static void SetDuration([GamePrefabID(typeof(ParticlePreset))] string id, float duration)
        {
            ResourcesManagementSetting.particleGeneralSetting.SetDuration(id, duration);
        }
    }
}
