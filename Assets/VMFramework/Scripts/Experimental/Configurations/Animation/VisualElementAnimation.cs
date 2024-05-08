using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public class VisualElementAnimation : BaseConfigClass
    {
        [LabelText("片段列表")]
        [SerializeField]
        private List<VisualElementAnimationClip> clipList = new();

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

        public void Run(VisualElement target)
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
                    if (target != null)
                    {
                        clip.Run(target);
                    }
                });

            }
        }

        public async UniTask RunAndAwaitForComplete(VisualElement target)
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
                    if (target != null)
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

        public void Kill(VisualElement target)
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
    }

    public abstract class VisualElementAnimationClip : BaseConfigClass
    {
        [LabelText("开始时间")]
        [MinValue(0)]
        public float startTime = 0;

        public abstract float GetTotalDuration();

        public virtual bool IsRequirementSatisfied(VisualElement target)
        {
            return true;
        }

        public abstract Tween Run(VisualElement target);

        public abstract void Kill(VisualElement target);

        public virtual void OnStart(VisualElement target)
        {

        }
    }

    public abstract class VisualElementFade : VisualElementAnimationClip
    {
        [LabelText("淡化时间")]
        [MinValue(0)]
        public float fadeDuration = 0.25f;

        [LabelText("动画曲线")]
        [Helper("https://easings.net/")]
        public Ease ease = Ease.Linear;

        public override float GetTotalDuration()
        {
            return fadeDuration;
        }

        public override void Kill(VisualElement target)
        {
            target.DOKill();
        }
    }

    [LabelText("淡入动画")]
    public class VisualElementFadeIn : VisualElementFade
    {
        [LabelText("开始时设置透明度为0")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToZeroOnStart = false;

        public override Tween Run(VisualElement target)
        {
            return target.DOFade(1, fadeDuration).SetEase(ease);
        }

        public override void OnStart(VisualElement target)
        {
            base.OnStart(target);

            if (setAlphaToZeroOnStart)
            {
                target.style.opacity = 0;
            }
        }
    }

    [LabelText("淡出动画")]
    public class VisualElementFadeOut : VisualElementFade
    {
        [LabelText("开始时设置透明度为1")]
        [LabelWidth(180)]
        [ToggleButtons("是", "否")]
        public bool setAlphaToOneOnStart = false;

        public override Tween Run(VisualElement target)
        {
            return target.DOFade(0, fadeDuration).SetEase(ease);
        }

        public override void OnStart(VisualElement target)
        {
            base.OnStart(target);

            if (setAlphaToOneOnStart)
            {
                target.style.opacity = 1;
            }
        }
    }
}
