using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public class SingleValueChooserConfig<T> : ChooserConfig<T>
    {
        [HideLabel]
        public T value;

        public SingleValueChooserConfig()
        {
            value = default;
        }
        
        public SingleValueChooserConfig(T value)
        {
            this.value = value;
        }

        public override IChooser<T> GenerateNewObjectChooser()
        {
            return new SingleValueChooser<T>(value);
        }

        public override IEnumerable<T> GetAvailableValues()
        {
            yield return value;
        }

        public override void SetAvailableValues(Func<T, T> setter)
        {
            value = setter(value);
        }

        public override string ToString()
        {
            return ValueToString(value);
        }

        public static implicit operator T(SingleValueChooserConfig<T> config)
        {
            return config.value;
        }
        
        public static implicit operator SingleValueChooserConfig<T>(T value)
        {
            return new SingleValueChooserConfig<T>(value);
        }
    }
}