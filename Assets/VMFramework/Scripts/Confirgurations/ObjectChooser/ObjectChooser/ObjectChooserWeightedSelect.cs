using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class ObjectChooser<T>
    {
        #region Fields

        [HideReferenceObjectPicker]
        [LabelText(@"@""按权值随机选择其中的一个"" + valueName")]
        [OnValueChanged(nameof(OnWeightedSelectItemsChanged), true)]
        [OnCollectionChanged(nameof(OnWeightedSelectItemsChanged))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddWeightedSelectItemGUI), NumberOfItemsPerPage = 6)]
        [ShowIf(nameof(isWeightedSelect))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public List<WeightedSelectItem> weightedSelectItems = new();

        #endregion
        
        #region GUI

        #region Add Weighted Select Item GUI

        protected virtual WeightedSelectItem AddWeightedSelectItemGUI()
        {
            return new()
            {
                ratio = 1
            };
        }

        #endregion

        #region Contains Same

        private bool requireToMergeDuplicates =>
            isWeightedSelect && IsWeightedSelectItemsContainsSameValue();
        
        private bool IsWeightedSelectItemsContainsSameValue()
        {
            return weightedSelectItems.ContainsSame(item => item.value);
        }
        
        [Button("合并相同的项")]
        [ShowIf(nameof(requireToMergeDuplicates))]
        private void ValueProbabilitiesMergeDuplicates()
        {
            weightedSelectItems = weightedSelectItems.MergeDuplicates(
                item => item.value,
                (itemA, itemB) =>
                {
                    itemA.ratio += itemB.ratio;
                    return itemA;
                }).ToList();

            OnWeightedSelectItemsChanged();
        }

        #endregion

        #region Remove All Null From Value Probabilities

        protected virtual bool displayRemoveAllNullFromValueProbabilitiesButton =>
            isWeightedSelect && typeof(T).IsClass;

        [Button("移除所有空值")]
        [ShowIf(nameof(displayRemoveAllNullFromValueProbabilitiesButton))]
        protected virtual void RemoveAllNullFromValueProbabilities()
        {
            weightedSelectItems.RemoveAll(item => item.value is null);
        }

        #endregion

        #region Ratios

        private bool IsWeightedSelectRatiosAllZero()
        {
            return weightedSelectItems.All(item => item.ratio <= 0);
        }

        private bool IsWeightedSelectRatiosCoprime()
        {
            return weightedSelectItems.Select(item => item.ratio).AreCoprime();
        }

        [Button("化简占比")]
        [ShowIf("@isWeightedSelect && !IsWeightedSelectRatiosCoprime()")]
        private void WeightedSelectRatiosToCoprime()
        {
            if (IsWeightedSelectRatiosAllZero())
            {
                weightedSelectItems.Examine(item => item.ratio = 1);
                return;
            }

            int gcd = weightedSelectItems.Select(item => item.ratio).GCD();
            if (gcd > 1)
            {
                weightedSelectItems.Examine(item => item.ratio /= gcd);
            }
        }

        #endregion

        #region On Weighted Select Items Changed

        protected void OnWeightedSelectItemsChanged()
        {
            if (weightedSelectItems == null || weightedSelectItems.Count == 0)
            {
                return;
            }

            weightedSelectItems.RemoveAllNull();

            foreach (var item in weightedSelectItems)
            {
                item.ratio = item.ratio.Clamp(weightedSelectItems.Count == 1 ? 1 : 0,
                    9999);
                item.probability = 0;
                item.tag = "";
            }

            var indicesOfMaxRatio = weightedSelectItems
                .GetIndicesOfMaxValues(item => item.ratio).ToList();
            var indicesOfMinRatio = weightedSelectItems
                .GetIndicesOfMinValues(item => item.ratio).ToList();

            var totalRatio = weightedSelectItems.Sum(item => item.ratio);

            if (totalRatio == 0)
            {
                return;
            }

            foreach (var index in indicesOfMaxRatio.Concat(indicesOfMinRatio))
            {
                var item = weightedSelectItems[index];

                bool isMax = indicesOfMaxRatio.Contains(index);
                bool isMin = indicesOfMinRatio.Contains(index);

                if (isMax && isMin)
                {
                    if (indicesOfMaxRatio.Count > 1)
                    {
                        item.tag += "一样";
                    }
                }
                else if (isMax)
                {
                    if (indicesOfMaxRatio.Count > 1)
                    {
                        item.tag += "同时";
                    }

                    item.tag += "最大";
                }
                else if (isMin)
                {
                    if (indicesOfMinRatio.Count > 1)
                    {
                        item.tag += "同时";
                    }

                    item.tag += "最小";
                }
            }

            foreach (var item in weightedSelectItems)
            {
                item.probability = 100f * item.ratio / totalRatio;
            }
        }

        #endregion

        #endregion

        #region Get Value

        private T GetWeightedSelectValue()
        {
            if (weightedSelectItems == null || weightedSelectItems.Count == 0)
            {
                return value;
            }

            List<T> values = new();
            List<float> ratios = new();

            foreach (var probabilityItem in weightedSelectItems)
            {
                values.Add(probabilityItem.value);
                ratios.Add(probabilityItem.ratio);
            }

            return values.Choose(ratios);
        }

        private IEnumerable<T> GetCurrentWeightedSelectValues()
        {
            return weightedSelectItems.Select(item => item.value);
        }

        #endregion

        #region To String

        private string WeightedSelectItemsToString()
        {
            if (weightedSelectItems.Count == 0)
            {
                return "";
            }

            if (weightedSelectItems.Count == 1)
            {
                return $"{ValueToString(weightedSelectItems[0].value)}";
            }

            var displayProbabilities = weightedSelectItems
                .Select(item => item.ratio).UniqueCount() != 1;

            return ", ".Join(weightedSelectItems.Select(item =>
            {
                var itemValueString = ValueToString(item.value);

                if (displayProbabilities)
                {
                    itemValueString +=
                        $":{item.probability.ToString(1)}%";
                }

                return itemValueString;
            }));
        }

        #endregion
    }
}