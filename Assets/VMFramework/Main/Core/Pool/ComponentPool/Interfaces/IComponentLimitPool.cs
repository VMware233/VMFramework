using UnityEngine;

namespace VMFramework.Core.Pool
{
    public interface IComponentLimitPool<T> : ILimitPool<T>, IComponentPool<T>
        where T : Component
    {

    }
}