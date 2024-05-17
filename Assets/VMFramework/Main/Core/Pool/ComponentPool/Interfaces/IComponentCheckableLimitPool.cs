using UnityEngine;

namespace VMFramework.Core.Pool
{
    public interface IComponentCheckableLimitPool<T> : ICheckableLimitPool<T>,
        IComponentLimitPool<T> where T : Component
    {

    }
}