using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Pool;

namespace Demo
{
    public class AudioSpawner : UniqueMonoBehaviour<AudioSpawner>
    {
        #region Config

        /// <summary>
        /// AudioSource组件的默认容器
        /// </summary>
        [SerializeField]
        private Transform audioSourceDefaultContainer;

        #endregion

        // 音频对象池
        [ShowInInspector]
        private static Dictionary<AudioClip, IComponentPool<AudioSource>>
            audioSourcePoolDictionary = new();

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="position">AudioSource组件所在的位置</param>
        /// <param name="autoCheckStop">是否在音频播放结束时自动Return给对象池</param>
        /// <param name="parent">AudioSource组件的父Transform</param>
        /// <returns></returns>
        [Button]
        public static AudioSource Play(AudioClip audioClip, Vector3 position,
            bool autoCheckStop, Transform parent = null)
        {
            if (audioClip == null)
            {
                Debug.LogError("AudioClip is null");
                return null;
            }

            var container = parent != null
                ? parent
                : instance.audioSourceDefaultContainer;

            if (audioSourcePoolDictionary.TryGetValue(audioClip,
                    out var audioSourcePool) == false)
            {
                audioSourcePool = new ComponentStackPool<AudioSource>();
                audioSourcePoolDictionary.Add(audioClip, audioSourcePool);
            }

            var audioSource =
                audioSourcePool.Get(container, false, out var isFreshlyCreated);

            if (isFreshlyCreated)
            {
                audioSource.clip = audioClip;
            }

            audioSource.transform.position = position;
            audioSource.loop = false;

            audioSource.Play();

            if (autoCheckStop)
            {
                _ = CheckStop(audioSource);
            }

            return audioSource;
        }

        /// <summary>
        /// 回收音效
        /// </summary>
        /// <param name="audioSource"></param>
        [Button]
        public static void Return(AudioSource audioSource)
        {
            audioSource.Stop();

            if (audioSource.gameObject.activeSelf)
            {
                audioSource.transform.SetParent(instance
                    .audioSourceDefaultContainer);

                if (audioSource.clip == null)
                {
                    Debug.LogError("AudioSource.clip is null");
                    return;
                }

                if (audioSourcePoolDictionary.TryGetValue(audioSource.clip,
                        out var audioSourcePool) == false)
                {
                    audioSourcePool = new ComponentStackPool<AudioSource>();
                    audioSourcePoolDictionary.Add(audioSource.clip, audioSourcePool);
                }

                audioSourcePool.Return(audioSource);
            }
        }

        /// <summary>
        /// 检查音频是否播放结束，如果结束则回收
        /// </summary>
        /// <param name="audioSource"></param>
        /// <returns></returns>
        private static async UniTaskVoid CheckStop(AudioSource audioSource)
        {
            await UniTask.WaitUntil(() => audioSource.isPlaying == false);

            Return(audioSource);
        }
    }
}
