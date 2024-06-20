using System;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Properties
{
    public interface IGameProperty : ILocalizedGameTypedGamePrefab
    {
        public Type targetType { get; }
        
        public Sprite icon { get; }
        
        public string GetValueString(object target);
    }
}