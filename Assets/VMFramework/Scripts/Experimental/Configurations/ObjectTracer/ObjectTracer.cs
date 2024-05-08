//using Sirenix.OdinInspector;
//using System;
//using System.Collections.Generic;
//using Basis.Core;
//using Basis.Utility;
//using Basis.Utility.Generic;
//using DG.Tweening;
//using UnityEngine;
//using UpdateType = Basis.Utility.UpdateType;

//namespace Basis.Configuration
//{
//    public enum ObjectTraceType
//    {
//        [LabelText("无动画")]
//        NoneAnimation,
//        [LabelText("插值")]
//        Lerp,
//        [LabelText("Dotween")]
//        Dotween
//    }

//    [Serializable]
//    [HideDuplicateReferenceBox]
//    [HideReferenceObjectPicker]
//    public class ObjectTracer<TObject> : ValuePreviewBase<TObject> where TObject : IEquatable<TObject>
//    {
//        protected override bool valueAlwaysPreviewed => true;
//        protected override bool showPreviewValueBelow => false;

//        [LabelText("追踪种类")]
//        [EnumToggleButtons]
//        [OnValueChanged(nameof(PreviewValue))]
//        public ObjectTraceType objectTraceType;

//        [LabelText("更新种类")]
//        [EnumToggleButtons]
//        public UpdateType updateType;

//        [LabelText("第一次设置值是否有动画")]
//        [HideIf("@objectTraceType == ObjectTraceType.NoneAnimation")]
//        public bool firstSetAnimation = false;

//        #region Lerp

//        [LabelText("插值速度")]
//        [MinValue(0.001), ShowIf("@objectTraceType == ObjectTraceType.Lerp")]
//        public float lerpSpeed = 5;

//        [LabelText("启用插值系数的幂")]
//        public bool enableLerpPower = false;

//        [LabelText("插值系数的幂")]
//        [MinValue(0.001), MaxValue(1000)]
//        [ShowIf("@objectTraceType == ObjectTraceType.Lerp && enableLerpPower")]
//        public float lerpPower = 1;

//        [LabelText("启用deltaTime修正插值系数")]
//        [ShowIf("@objectTraceType == ObjectTraceType.Lerp")]
//        public bool enableLerpSpeedModifiedByTimeDelta = true;

//        #endregion

//        #region Dotween

//        [LabelText("速度")]
//        [MinValue(0.001), ShowIf("@objectTraceType == ObjectTraceType.Dotween")]
//        public float dotweenSpeed = 1;

//        [LabelText("曲线")]
//        [ShowIf("@objectTraceType == ObjectTraceType.Dotween")]
//        public Ease ease;

//        #endregion

//        [LabelText("改变阈值")]
//        [MinValue(0)]
//        public float changeThreshold = 0.1f;

//        private TObject _value;

//        [LabelText("当前值"), FoldoutGroup("Debugging", Order = 100)]
//        [ShowIf("@GameCoreSettingBase.objectTracerGeneralSetting.isDebugging == true")]
//        [ShowInInspector]
//        public TObject value
//        {
//            get => _value; 
//            set
//            {
//                _value = value;
//                OnValueChanged?.Invoke();
//            }
//        }
//        [LabelText("目标值"), FoldoutGroup("Debugging")]
//        [ShowIf("@GameCoreSettingBase.objectTracerGeneralSetting.isDebugging == true")]
//        [ShowInInspector]
//        public TObject targetValue { get; private set; }
//        [LabelText("第一次设置值"), FoldoutGroup("Debugging")]
//        [ShowIf("@GameCoreSettingBase.objectTracerGeneralSetting.isDebugging == true")]
//        [ShowInInspector, DisplayAsString]
//        private bool isFirstSet = true;

//        /// <summary>
//        /// 当值改变时触发
//        /// </summary>
//        public event Action OnValueChanged;

//        #region GUI

//        protected override void PreviewValue()
//        {
//            base.PreviewValue();

//            contentPreviewing = objectTraceType.GetEnumLabel();
//        }

//        #endregion

//        protected override void OnInit()
//        {
//            base.OnInit();

//            UpdateDelegateManager.AddUpdateDelegate(updateType, Update);
//        }

//        private void Update()
//        {
//            switch (objectTraceType)
//            {
//                case ObjectTraceType.NoneAnimation:

//                    if (targetValue.Distance(value) > changeThreshold)
//                    {
//                        value = targetValue;
//                    }

//                    break;
//                case ObjectTraceType.Lerp:

//                    if (targetValue.Distance(value) > changeThreshold)
//                    {
//                        float actualLerpSpeed = enableLerpSpeedModifiedByTimeDelta ? 
//                            lerpSpeed * Time.deltaTime : 
//                            lerpSpeed;

//                        value = enableLerpPower ? 
//                            value.Lerp(targetValue, actualLerpSpeed, lerpPower) : 
//                            value.Lerp(targetValue, actualLerpSpeed);
//                    }

//                    break;
//                case ObjectTraceType.Dotween:
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }


//        }



//        public void SetValue(TObject newValue)
//        {
//            if (isFirstSet)
//            {
//                isFirstSet = false;

//                if (firstSetAnimation == false)
//                {
//                    value = newValue;
//                }
//            }
//            targetValue = newValue;

//            if (objectTraceType == ObjectTraceType.Dotween)
//            {
//                DOTweenExtension.To(() => value, newValue => value = newValue, targetValue,
//                    value.Distance(targetValue).F() / 200 * dotweenSpeed).SetTarget(this).SetEase(ease);
//            }
//            else
//            {
//                DOTween.Kill(this);
//            }
//        }

//        public void SetValue(float gap, IEnumerable<TObject> enumerable)
//        {
//            gap.DelayAction(enumerable, SetValue);
//        }
//    }
//}

