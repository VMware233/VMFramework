using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GamePrefabWrapper : SerializedScriptableObject, INameOwner
    {
        public abstract IEnumerable<IGamePrefab> GetGamePrefabs();
        
        public abstract void InitGamePrefabs(IEnumerable<IGamePrefab> gamePrefabs);

        string INameOwner.name => this == null ? "Null" : name;
    }
}
