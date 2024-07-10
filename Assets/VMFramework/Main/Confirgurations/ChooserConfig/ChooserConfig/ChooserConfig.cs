using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract class ChooserConfig<TItem> : ChooserConfig<TItem, TItem>, IChooserConfig<TItem>
    {
        protected sealed override TItem UnboxWrapper(TItem wrapper)
        {
            return wrapper;
        }
    }
    
    [PreviewComposite]
    public abstract class ChooserConfig<TWrapper, TItem> : BaseConfig, IChooserConfig<TWrapper, TItem>
    {
        [ShowInInspector, HideInEditorMode]
        private IChooser<TItem> objectChooser;

        protected override void OnInit()
        {
            base.OnInit();
            
            objectChooser = GenerateNewObjectChooser();
        }

        public TItem GetValue()
        {
            return objectChooser.GetValue();
        }

        public IChooser<TItem> GetObjectChooser()
        {
            return objectChooser;
        }

        public void RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        public virtual IEnumerable<TItem> GetAvailableValues()
        {
            return GetAvailableWrappers().Select(UnboxWrapper);
        }

        public abstract IEnumerable<TWrapper> GetAvailableWrappers();

        public abstract void SetAvailableValues(Func<TWrapper, TWrapper> setter);

        public abstract IChooser<TItem> GenerateNewObjectChooser();

        protected virtual string ValueToString(TWrapper value)
        {
            if (value is IEnumerable<object> enumerable)
            {
                return $"[{enumerable.Join(";")}]";
            }
            return value?.ToString();
        }

        protected abstract TItem UnboxWrapper(TWrapper wrapper);
    }
}