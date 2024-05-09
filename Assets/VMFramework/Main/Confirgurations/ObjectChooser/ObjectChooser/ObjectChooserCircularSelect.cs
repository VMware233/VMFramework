using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        #region Group

        protected const string CIRCULAR_SELECT_INFO_GROUP = "信息";

        #endregion
        
        #region Fields

        [LabelText("从第几个开始循环")]
        [PropertyTooltip("从0开始计数")]
        [ShowIf(nameof(isCircularSelect))]
        [MinValue(0)]
        [JsonProperty]
        public int startCircularIndex = 0;

        [LabelText("乒乓循环")]
        [PropertyTooltip("循环到底后，从后往前遍历")]
        [ShowIf(nameof(canShowPingPongOption))]
        [JsonProperty]
        public bool pingPong = false;

        [LabelText("循环体")]
        [HideReferenceObjectPicker]
        [ShowIf(nameof(isCircularSelect))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddCircularSelectItemGUI), ShowFoldout = true)]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public List<CircularSelectItem> circularSelectItems = new();

        [LabelText("当前循环的序号"), FoldoutGroup(CIRCULAR_SELECT_INFO_GROUP)]
        [ShowInInspector, DisplayAsString, HideInEditorMode]
        [ShowIf(nameof(isCircularSelect))]
        [NonSerialized]
        protected int currentCircularIndex = 0;

        [LabelText("当前循环的序号下的次数"), FoldoutGroup(CIRCULAR_SELECT_INFO_GROUP)]
        [ShowInInspector, DisplayAsString, HideInEditorMode]
        [ShowIf(nameof(isCircularSelect))]
        [NonSerialized]
        protected int currentCircularTimes = 1;

        [LabelText("当前循环方向"), FoldoutGroup(CIRCULAR_SELECT_INFO_GROUP)]
        [ShowInInspector, DisplayAsString, HideInEditorMode]
        [ShowIf(nameof(canShowLoopForwardOption))]
        [NonSerialized]
        protected bool loopForward = true;

        #endregion
        
        #region GUI

        private bool canShowPingPongOption => isCircularSelect &&
                                              circularSelectItems is { Count: > 2 };

        private bool canShowLoopForwardOption => canShowPingPongOption && pingPong;

        protected const string CIRCULAR_ACTIONS_CATEGORY = "CircularActions";

        private CircularSelectItem AddCircularSelectItemGUI()
        {
            CircularSelectItem item = new()
            {
                index = circularSelectItems.Count,
                times = 1,
                value = default,
            };

            return item;
        }

        [Button("上移")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        [ShowIf(nameof(isCircularSelect))]
        private void ShiftUpCircularSelectItems()
        {
            circularSelectItems.Rotate(-1);
        }

        [Button("下移")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        [ShowIf(nameof(isCircularSelect))]
        private void ShiftDownCircularSelectItems()
        {
            circularSelectItems.Rotate(1);
        }

        [Button("打乱")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        [ShowIf(nameof(isCircularSelect))]
        private void ShuffleCircularSelectItems()
        {
            circularSelectItems.Shuffle();
        }

        [Button("重置循环次数")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        [ShowIf(nameof(isCircularSelect))]
        private void ResetCircularSelectItemsTimes()
        {
            foreach (var item in circularSelectItems)
            {
                item.times = 1;
            }
        }

        #endregion

        #region Get Value

        private T GetCircularSelectValue()
        {
            if (circularSelectItems.Count == 0)
            {
                return default;
            }

            var item = circularSelectItems[currentCircularIndex];

            if (pingPong == false)
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;
                    currentCircularIndex++;

                    if (currentCircularIndex >= circularSelectItems.Count)
                    {
                        currentCircularIndex = startCircularIndex;
                    }
                }
            }
            else
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;

                    if (loopForward)
                    {
                        currentCircularIndex++;

                        if (currentCircularIndex >= circularSelectItems.Count)
                        {
                            currentCircularIndex = circularSelectItems.Count - 2;
                            loopForward = false;
                        }
                    }
                    else
                    {
                        if (currentCircularIndex <= startCircularIndex)
                        {
                            currentCircularIndex++;
                            loopForward = true;
                        }
                        else
                        {
                            currentCircularIndex--;
                        }
                    }

                }
            }

            return item.value;
        }

        #endregion

        #region Get Current Values

        private IEnumerable<T> GetCurrentCircularSelectValues()
        {
            return circularSelectItems.Select(item => item.value);
        }

        #endregion

        #region To String

        private string CircularSelectItemsToString()
        {
            var content = ", ".Join(circularSelectItems.Select(item =>
            {
                if (item.times > 1)
                {
                    return $"{ValueToString(item.value)}:{item.times}次";
                }

                return ValueToString(item.value);
            }));

            if (pingPong)
            {
                content += " 乒乓循环";
            }

            return content;
        }

        #endregion
    }
}