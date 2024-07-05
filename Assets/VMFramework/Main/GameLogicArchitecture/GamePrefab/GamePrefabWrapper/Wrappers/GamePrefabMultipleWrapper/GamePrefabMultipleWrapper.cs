using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.GameLogicArchitecture
{
    [CreateAssetMenu(fileName = "New GamePrefabMultipleWrapper", 
        menuName = "VMFramework/GamePrefabMultipleWrapper")]
    public sealed partial class GamePrefabMultipleWrapper : GamePrefabWrapper, INameOwner
    {
        public List<IGamePrefab> gamePrefabs = new();

        public override IEnumerable<IGamePrefab> GetGamePrefabs()
        {
            return gamePrefabs;
        }

        public override void InitGamePrefabs(IEnumerable<IGamePrefab> gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs.ToList();
        }

        string INameOwner.name
        {
            get
            {
                if (gamePrefabs.IsNullOrEmpty() || gamePrefabs[0] == null)
                {
                    if (this != null)
                    {
                        return name;
                    }

                    return $"Null {nameof(GamePrefabMultipleWrapper)}";
                }

                return gamePrefabs[0].name;
            }
        }
    }
}