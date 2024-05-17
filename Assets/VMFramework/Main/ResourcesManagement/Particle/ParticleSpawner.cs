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
        private static readonly Dictionary<string, IComponentPool<ParticleSystem>> allPools =
            new();

        private static readonly Dictionary<ParticleSystem, string> allParticleIDs = new();

        /// <summary>
        /// 回收粒子
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

                particle.transform.SetParent(GameCoreSettingBase.particleGeneralSetting.container);
                pool.Return(particle);
            }
        }

        /// <summary>
        /// 生成粒子
        /// 如果父Transform为Null，则为位置参数为world space position，如若不然，则是local position
        /// </summary>
        /// <param name="id">粒子ID</param>
        /// <param name="pos">位置</param>
        /// <param name="parent">父Transform</param>
        /// <param name="isWorldSpace"></param>
        /// <returns></returns>
        [Button("生成粒子")]
        public static ParticleSystem Spawn(
            [GamePrefabID(typeof(ParticlePreset))]
            string id, Vector3 pos, Transform parent = null, bool isWorldSpace = true)
        {
            var registeredParticle = GamePrefabManager.GetGamePrefabStrictly<ParticlePreset>(id);
            
            if (allPools.TryGetValue(id, out var pool) == false)
            {
                pool = new ComponentStackPool<ParticleSystem>();
                allPools[id] = pool;
            }

            var container = parent == null
                ? GameCoreSettingBase.particleGeneralSetting.container
                : parent;

            var newParticleSystem = pool.Get(registeredParticle.particlePrefab, container);

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
                registeredParticle.duration.GetValue().DelayAction(() =>
                {
                    Return(newParticleSystem);
                });
            }

            return newParticleSystem;
        }

        [Button("设置持续时间")]
        public static void SetDuration(
            [GamePrefabID(typeof(ParticlePreset))]
            string id, float duration)
        {
            GameCoreSettingBase.particleGeneralSetting.SetDuration(id,
                duration);
        }
    }
}
