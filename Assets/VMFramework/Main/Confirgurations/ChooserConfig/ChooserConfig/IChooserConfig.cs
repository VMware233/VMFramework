using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IChooserConfig : IBaseConfigClass
    {
        public void Init();
        
        public void RegenerateObjectChooser();
        
        public object GetValue();
        
        public IChooser GetObjectChooser();
        
        public IChooser GenerateNewObjectChooser();

        public IEnumerable GetAvailableValues();
    }
    
    public interface IChooserConfig<T> : IChooserConfig
    {
        public new T GetValue();

        object IChooserConfig.GetValue()
        {
            return GetValue();
        }

        public new IChooser<T> GetObjectChooser();

        IChooser IChooserConfig.GetObjectChooser()
        {
            return GetObjectChooser();
        }

        public new IChooser<T> GenerateNewObjectChooser();

        IChooser IChooserConfig.GenerateNewObjectChooser()
        {
            return GenerateNewObjectChooser();
        }

        public new IEnumerable<T> GetAvailableValues();

        IEnumerable IChooserConfig.GetAvailableValues()
        {
            return GetAvailableValues();
        }

        public void SetAvailableValues(Func<T, T> setter);
    }
}