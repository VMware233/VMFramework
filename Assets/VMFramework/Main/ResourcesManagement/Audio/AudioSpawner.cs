using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VMFramework.Core.Pool;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class AudioSpawner : SerializedMonoBehaviour
    {
        private static IComponentPool<AudioSource> pool = new ComponentStackPool<AudioSource>();

        public static void Return(AudioSource audioSource)
        {
            audioSource.Stop();

            if (audioSource.gameObject.activeSelf)
            {
                audioSource.transform.SetParent(GameCoreSettingBase
                    .audioGeneralSetting.container);
                pool.Return(audioSource);
            }
        }

        [Button("生成音效")]
        public static AudioSource Spawn(
            [ValueDropdown(
                "@GameCoreSettingBase.audioGeneralSetting.GetPrefabNameList()")]
            string id, Vector3 pos, Transform parent = null)
        {
            var preset = GamePrefabManager.GetGamePrefabStrictly<AudioPreset>(id);

            var audioSource = pool.Get(() =>
            {
                var gameObject = new GameObject();

                return gameObject.AddComponent<AudioSource>();
            });

            audioSource.clip = preset.audioClip;
            audioSource.name = preset.id;
            audioSource.volume = preset.volume;
            audioSource.loop = false;
            audioSource.time = 0;

            var container = parent == null
                ? GameCoreSettingBase.audioGeneralSetting.container
                : parent;

            audioSource.transform.SetParent(container);
            audioSource.transform.position = pos;

            if (preset.autoCheckStop)
            {
                _ = CheckStop(audioSource);
            }

            if (preset.enablePlayFromTime)
            {
                audioSource.time = preset.timeToPlay;
            }

            audioSource.Play();

            return audioSource;
        }

        private static async UniTaskVoid CheckStop(AudioSource audioSource)
        {
            await UniTask.WaitUntil(() => audioSource.isPlaying == false);

            Return(audioSource);
        }
    }
}
