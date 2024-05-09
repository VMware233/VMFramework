using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Configuration;
using VMFramework.Core;
using UnityEngine;
using Newtonsoft.Json;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public class TrailSpawnConfig : BaseConfigClass
    {
        [LabelText("拖尾效果")]
        [GamePrefabIDValueDropdown(typeof(TrailPreset))]
        [IsNotNullOrEmpty]
        [SerializeField]
        protected string trailID;

        [LabelText("位置")]
        [SerializeField, JsonProperty]
        protected Vector3SetterCore position = 0;

        public TrailRenderer GenerateTrail(Transform parent)
        {
            var trailRenderer = TrailSpawner.Spawn(trailID, position, parent);

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
            trail.transform.SetParent(GameCoreSettingBase.trailGeneralSetting
                .container);
        }

        /// <summary>
        /// 回收粒子
        /// </summary>
        /// <param name="trail"></param>
        public static void Return(TrailRenderer trail)
        {
            if (trail.gameObject.activeSelf)
            {
                var id = allTrailIDs[trail];
                var pool = allPools[id];

                trail.transform.SetParent(GameCoreSettingBase.trailGeneralSetting
                    .container);
                pool.Return(trail);
            }
        }

        /// <summary>
        /// 生成粒子
        /// 如果父Transform为Null，则为位置参数为world space position，如若不然，则是local position
        /// </summary>
        /// <param name="id">粒子ID</param>
        /// <param name="pos">位置</param>
        /// <param name="parent">父Transform</param>
        /// <returns></returns>
        public static TrailRenderer Spawn(string id, Vector3 pos,
            Transform parent = null)
        {
            var registeredTrail = GamePrefabManager.GetGamePrefabStrictly<TrailPreset>(id);
            
            if (allPools.TryGetValue(id, out var pool) == false)
            {
                pool = new ComponentStackPool<TrailRenderer>();
                allPools[id] = pool;
            }

            var container = parent == null
                ? GameCoreSettingBase.trailGeneralSetting.container
                : parent;

            var newTrail = pool.Get(registeredTrail.trailPrefab, container);

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
