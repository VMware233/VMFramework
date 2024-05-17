using UnityEngine;

namespace VMFramework.Core.Pool
{
    public interface IComponentCheckablePool<T> : ICheckablePool<T>,
        IComponentPool<T> where T : Component
    {

    }
}