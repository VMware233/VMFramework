using System.Collections.Generic;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class GamePrefabWrapper : GameEditorScriptableObject, INameOwner
    {
        public abstract IEnumerable<IGamePrefab> GetGamePrefabs();
        
        public abstract void InitGamePrefabs(IEnumerable<IGamePrefab> gamePrefabs);

        string INameOwner.name => this == null ? "Null" : name;
    }
}
