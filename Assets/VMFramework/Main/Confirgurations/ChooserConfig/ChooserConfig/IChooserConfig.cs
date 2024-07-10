using System;
using System.Collections;
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IChooserConfig : IConfig, IChooser, IChooserGenerator
    {
        public void RegenerateObjectChooser();
        
        public IChooser GenerateNewObjectChooser();

        public IEnumerable GetAvailableValues();

        void IChooser.ResetChooser()
        {
            // No need to reset the chooser since this is a configuration.
        }
    }

    public interface IChooserConfig<TItem> : IChooserConfig<TItem, TItem>
    {
        
    }
    
    public interface IChooserConfig<TWrapper, TItem> : IChooserConfig, IChooser<TItem>, IChooserGenerator<TItem>
    {
        public new IChooser<TItem> GenerateNewObjectChooser();

        IChooser IChooserConfig.GenerateNewObjectChooser()
        {
            return GenerateNewObjectChooser();
        }

        public new IEnumerable<TItem> GetAvailableValues();

        IEnumerable IChooserConfig.GetAvailableValues()
        {
            return GetAvailableValues();
        }

        public IEnumerable<TWrapper> GetAvailableWrappers();

        public void SetAvailableValues(Func<TWrapper, TWrapper> setter);
    }
}