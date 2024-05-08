using System;
using System.Linq;
using System.Collections.Generic;
using VMFramework.Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [Serializable]
    public class ObjectAnimation : BaseConfigClass
    {
        [LabelText("片段列表")]
        [SerializeField]
        private List<ObjectAnimationClip> clipList = new();

        private float _totalDuration = 0;

        [LabelText("总持续时间")]
        [ShowInInspector]
        public float totalDuration
        {
            get
            {
                if (initDone)
                {
                    return _totalDuration;
                }

                return GetTotalDuration();
            }
        }

        protected override void OnInit()
        {
            base.OnInit();

            _totalDuration = GetTotalDuration();
        }

        public void Run(Transform target)
        {
            if (clipList.All(clip => clip.IsRequirementSatisfied(target)) == false)
            {
                Debug.LogWarning($"{target} 不满足动画片段需求，无法播放");
                return;
            }

            foreach (var clip in clipList)
            {
                clip.OnStart(target);

                clip.startTime.DelayAction(() =>
                {
                    if (target != null && target.gameObject.activeSelf)
                    {
                        clip.Run(target);
                    }
                });

            }
        }

        public async UniTask RunAndAwaitForComplete(Transform target)
        {
            if (clipList.All(clip => clip.IsRequirementSatisfied(target)) == false)
            {
                Debug.LogWarning($"{target} 不满足动画片段需求，无法播放");
                return;
            }

            int completeCount = 0;
            foreach (var clip in clipList)
            {
                clip.OnStart(target);

                clip.startTime.DelayAction(() =>
                {
                    if (target != null && target.gameObject.activeSelf)
                    {
                        clip.Run(target).OnComplete(() => completeCount++);
                    }
                });
            }

            await UniTask.WaitUntil(() => completeCount == clipList.Count);
        }

        private float GetTotalDuration()
        {
            if (clipList == null || clipList.Count == 0)
            {
                return 0;
            }

            float totalDuration = 0;
            foreach (var clip in clipList)
            {
                if (clip == null)
                {
                    continue;
                }

                totalDuration =
                    totalDuration.Max(clip.startTime + clip.GetTotalDuration());
            }

            return totalDuration;
        }

        public void Kill(Transform target)
        {
            if (target == null)
            {
                return;
            }

            foreach (var clip in clipList)
            {
                clip.Kill(target);
            }
        }

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            clipList ??= new();
        }

        public bool IsEmpty()
        {
            return clipList == null || clipList.Count == 0;
        }

        #region Preset

        [FoldoutGroup("预设")]
        [Button("跳跃淡出预设")]
        private void AddLeapFadeOutPreset()
        {
            clipList ??= new();

            clipList.Add(new FadeOut()
            {
                startTime = 0.3f,
                fadeDuration = 0.25f,
                setAlphaToOneOnStart = true
            });

            clipList.Add(new Leap()
            {
                startTime = 0,
                leapDuration = 0.7f,
                leapPower = 50,
                leapTimes = 2,
                leapEndOffset = new()
                {
                    asVector2D = true,
                    planeType = PlaneType.XY,
                    setter2D = new()
                    {
                        isRandomValue = true,
                        randomType = "Weighted Select",
                        weightedSelectItems = new()
                        {
                            new()
                            {
                                value = new Vector2(140, 50),
                                ratio = 1
                            },
                            new()
                            {
                                value = new Vector2(-140, 50),
                                ratio = 1
                            }
                        },
                        decimalPlaces = 0
                    }
                }
            });
        }

        [FoldoutGroup("预设")]
        [Button("从底下浮现并淡出预设")]
        private void AddRiseAndFadeOutPreset()
        {
            clipList ??= new();

            clipList.Add(new FadeIn()
            {
                startTime = 0,
                fadeDuration = 0.25f,
                setAlphaToZeroOnStart = true,
            });

            clipList.Add(new Move()
            {
                startTime = 0,
                moveDuration = 0.3f,
                end = new Vector2(0, 65),
                ease = Ease.OutCubic
            });

            clipList.Add(new FadeOut()
            {
                startTime = 0.3f,
                fadeDuration = 0.2f,
                setAlphaToOneOnStart = false
            });
        }

        #endregion
    }

    public abstract class ObjectAnimationClip : BaseConfigClass
    {
        [LabelText("开始时间")]
        [MinValue(0)]
        public float startTime = 0;

        public abstract float GetTotalDuration();

        public virtual bool IsRequirementSatisfied(Transform target)
        {
            return true;
        }

        public abstract Tween Run(Transform target);

        public abstract void Kill(Transform target);

        public virtual void OnStart(Transform target)
        {

        }
    }

    public abstract class Fade : ObjectAnimationClip
    {
        [LabelText("淡化时间")]
        [MinValue(0)]
        public float fadeDuration = 0.25f;

        public override float GetTotalDuration()
        {
            return fadeDuration;
        }

        public override bool IsRequirementSatisfied(Transform target)
        {
            if (target.GetComponent<CanvasGroup>() == null)
            {
                Debug.LogWarning($"{target}没有CanvasGroup组件，无法播放淡化动画");
                return false;
            }

            return true;
        }

        public override void OnStart(Transform target)
        {
            base.OnStart(target);

            var canvasGroup = target.GetComponent<CanvasGroup>();

            OnStart(canvasGroup);
        }

        public override Tween Run(Transform target)
        {
            var canvasGroup = target.GetComponent<CanvasGroup>();

            return Run(canvasGroup);
        }

        public override void Kill(Transform target)
        {
            var canvasGroup = target.GetComponent<CanvasGroup>();

            canvasGroup.DOKill();
        }

        protected abstract Tween Run(CanvasGroup canvasGroup);

        protected abstract void OnStart(CanvasGroup canvasGroup);
    }

    [LabelText("淡入动画")]
    public class FadeIn : Fade
    {
        [LabelText("开始时设置透明度为0")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToZeroOnStart = false;

        protected override Tween Run(CanvasGroup canvasGroup)
        {
            return canvasGroup.DOFade(1, fadeDuration);
        }

        protected override void OnStart(CanvasGroup canvasGroup)
        {
            if (setAlphaToZeroOnStart)
            {
                canvasGroup.alpha = 0;
            }
        }
    }

    [LabelText("淡出动画")]
    public class FadeOut : Fade
    {
        [LabelText("开始时设置透明度为1")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToOneOnStart = false;

        protected override Tween Run(CanvasGroup canvasGroup)
        {
            return canvasGroup.DOFade(0, fadeDuration);
        }

        protected override void OnStart(CanvasGroup canvasGroup)
        {
            if (setAlphaToOneOnStart)
            {
                canvasGroup.alpha = 1;
            }
        }
    }

    [LabelText("跳跃动画")]
    public class Leap : ObjectAnimationClip
    {
        [LabelText("跳跃持续时间")]
        [MinValue(0)]
        public float leapDuration = 0.7f;

        [LabelText("跳跃落点")]
        public Vector3Setter leapEndOffset = new Vector2(140, 50);

        [LabelText("跳跃力量")]
        [MinValue(0)]
        public FloatSetter leapPower = 50;

        [LabelText("跳跃次数")]
        [MinValue(0)]
        public IntegerSetter leapTimes = 2;


        public override float GetTotalDuration()
        {
            return leapDuration;
        }

        public override Tween Run(Transform target)
        {
            return target.DOLocalJump(target.localPosition + leapEndOffset,
                leapPower,
                leapTimes,
                leapDuration,
                false);
        }

        public override void Kill(Transform target)
        {
            target.DOKill();
        }

        #region GUI

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            leapEndOffset ??= new();
            leapPower ??= new();
            leapTimes ??= new();
        }

        #endregion
    }

    [LabelText("移动动画")]
    public class Move : ObjectAnimationClip
    {
        [LabelText("移动持续时间")]
        [MinValue(0)]
        public float moveDuration = 0.3f;

        [LabelText("终点")]
        public Vector3Setter end = new();

        [LabelText("动画曲线")]
        [Helper("https://easings.net/")]
        public Ease ease = Ease.Linear;

        public override float GetTotalDuration()
        {
            return moveDuration;
        }

        public override Tween Run(Transform target)
        {
            return target.DOLocalMove(target.localPosition + end, moveDuration)
                .SetEase(ease);
        }

        public override void Kill(Transform target)
        {
            target.DOKill();
        }
    }
}
