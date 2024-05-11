using System;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public abstract class ChooserConfig<T> : BaseConfigClass, IChooserConfig<T>
    {
        private IChooser<T> objectChooser;

        public override void Init()
        {
            base.Init();
            
            objectChooser = GenerateNewObjectChooser();
        }

        public T GetValue()
        {
            return objectChooser.GetValue();
        }

        public IChooser<T> GetObjectChooser()
        {
            return objectChooser;
        }

        public void RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        public abstract IEnumerable<T> GetAvailableValues();
        
        public abstract void SetAvailableValues(Func<T, T> setter);

        public abstract IChooser<T> GenerateNewObjectChooser();

        protected virtual string ValueToString(T value)
        {
            return value.ToString();
        }
    }
}