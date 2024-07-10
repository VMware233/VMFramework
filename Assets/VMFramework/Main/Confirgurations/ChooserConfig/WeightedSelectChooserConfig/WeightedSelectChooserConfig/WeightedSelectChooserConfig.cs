using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public class WeightedSelectChooserConfig<TItem> : WeightedSelectChooserConfig<TItem, TItem>, 
        IWeightedSelectChooserConfig<TItem>
    {
        public WeightedSelectChooserConfig() : base()
        {
            
        }
        
        public WeightedSelectChooserConfig(IEnumerable<TItem> items) : base(items)
        {
            
        }
        
        protected sealed override TItem UnboxWrapper(TItem wrapper)
        {
            return wrapper;
        }
    }
    
    [TypeInfoBox("Choose a value from weighted items!")]
    public abstract partial class WeightedSelectChooserConfig<TWrapper, TItem>
        : ChooserConfig<TWrapper, TItem>, IWeightedSelectChooserConfig<TWrapper, TItem>
    {
#if UNITY_EDITOR
        [OnValueChanged(nameof(OnItemsChangedGUI), true)]
        [OnCollectionChanged(nameof(OnItemsChangedGUI))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddWeightedSelectItemGUI), NumberOfItemsPerPage = 6)]
#endif
        [IsNotNullOrEmpty]
        [JsonProperty]
        public List<WeightedSelectItemConfig<TWrapper>> items = new();

        protected WeightedSelectChooserConfig()
        {

        }

        protected WeightedSelectChooserConfig(IEnumerable<TWrapper> items)
        {
            this.items = items.Select(item => new WeightedSelectItemConfig<TWrapper>
            {
                value = item,
                ratio = 1
            }).ToList();
        }

        protected override void OnInit()
        {
            base.OnInit();

            foreach (var item in items)
            {
                if (item.value is IConfig config)
                {
                    config.Init();
                }
                else if (item.value is IEnumerable enumerable)
                {
                    foreach (var obj in enumerable)
                    {
                        if (obj is IConfig configObj)
                        {
                            configObj.Init();
                        }
                    }
                }
            }
        }

        public override IChooser<TItem> GenerateNewObjectChooser()
        {
            return new WeightedSelectChooser<TItem>(items
                .Select(item => (UnboxWrapper(item.value), item.ratio.F())).ToArray());
        }

        public override IEnumerable<TWrapper> GetAvailableWrappers()
        {
            return items.Select(item => item.value);
        }

        public override void SetAvailableValues(Func<TWrapper, TWrapper> setter)
        {
            foreach (var item in items)
            {
                item.value = setter(item.value);
            }
        }

        public bool ContainsWrapper(TWrapper wrapper)
        {
            return items.Any(item => item.value.Equals(wrapper));
        }

        public void AddWrapper(TWrapper wrapper)
        {
            items.Add(new WeightedSelectItemConfig<TWrapper>
            {
                value = wrapper,
                ratio = 1
            });

#if UNITY_EDITOR
            OnItemsChangedGUI();
#endif
        }

        public void RemoveWrapper(TWrapper wrapper)
        {
            items.RemoveAll(item => item.value.Equals(wrapper));
#if UNITY_EDITOR
            OnItemsChangedGUI();
#endif
        }

        public override string ToString()
        {
            if (items.Count == 0)
            {
                return "";
            }

            if (items.Count == 1)
            {
                return $"{ValueToString(items[0].value)}";
            }

            var displayProbabilities = items.Select(item => item.ratio).UniqueCount() != 1;

            return ", ".Join(items.Select(item =>
            {
                var itemValueString = ValueToString(item.value);

                if (displayProbabilities)
                {
                    itemValueString += $":{item.probability.ToString(1)}%";
                }

                return itemValueString;
            }));
        }
    }
}