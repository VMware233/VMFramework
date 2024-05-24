using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Pool;

namespace VMFramework.Examples
{
    public class AudioSpawner : UniqueMonoBehaviour<AudioSpawner>
    {
        #region Config

        /// <summary>
        /// The default transform parent for the AudioSource components.
        /// </summary>
        [SerializeField]
        private Transform audioSourceDefaultContainer;

        #endregion

        // The pool dictionary for the AudioSource components.
        [ShowInInspector]
        private static Dictionary<AudioClip, IComponentPool<AudioSource>>
            audioSourcePoolDictionary = new();

        private static IComponentPool<AudioSource> CreatePool(AudioClip audioClip)
        {
            return new StackComponentPool<AudioSource>(() =>
            {
                var audioSource = new GameObject(audioClip.name).AddComponent<AudioSource>();
                audioSource.clip = audioClip;
                return audioSource;
            }, onReturnCallback: audioSource =>
            {
                audioSource.transform.SetParent(instance.audioSourceDefaultContainer);
                audioSource.SetActive(false);
            });
        }
        
        [Button]
        public static AudioSource Play(AudioClip audioClip, Vector3 position,
            bool autoCheckStop, Transform parent = null)
        {
            if (audioClip == null)
            {
                Debug.LogError("AudioClip is null");
                return null;
            }

            if (audioSourcePoolDictionary.TryGetValue(audioClip, out var audioSourcePool) == false)
            {
                audioSourcePool = CreatePool(audioClip);
                audioSourcePoolDictionary.Add(audioClip, audioSourcePool);
            }

            var audioSource = audioSourcePool.Get(parent);

            audioSource.transform.position = position;
            audioSource.loop = false;

            audioSource.Play();

            if (autoCheckStop)
            {
                _ = CheckStop(audioSource);
            }

            return audioSource;
        }

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
                    audioSourcePool = CreatePool(audioSource.clip);
                    audioSourcePoolDictionary.Add(audioSource.clip, audioSourcePool);
                }

                audioSourcePool.Return(audioSource);
            }
        }

        /// <summary>
        /// Check if the audio has ended and return it if it has.
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
