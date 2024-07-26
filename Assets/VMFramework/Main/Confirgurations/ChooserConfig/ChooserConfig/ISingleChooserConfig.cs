using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface ISingleChooserConfig<T> : IChooserConfig<T>
    {
        protected IChooser<T> objectChooser { get; set; }

        void IInitializableConfig.Init()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        T IChooser<T>.GetValue()
        {
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                objectChooser ??= GenerateNewObjectChooser();
            }
#endif

            return objectChooser.GetValue();
        }

        IChooser<T> IChooserGenerator<T>.GetObjectChooser()
        {
            return objectChooser;
        }

        void IChooserConfig.RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }
    }
}