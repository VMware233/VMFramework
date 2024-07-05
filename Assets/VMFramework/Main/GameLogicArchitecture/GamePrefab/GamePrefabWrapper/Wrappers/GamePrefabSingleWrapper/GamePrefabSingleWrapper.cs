using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameLogicArchitecture
{
    [CreateAssetMenu(fileName = "New GamePrefabSingleWrapper", 
        menuName = "VMFramework/GamePrefabSingleWrapper")]
    public sealed partial class GamePrefabSingleWrapper : GamePrefabWrapper, INameOwner
    {
        [HideLabel]
        public IGamePrefab gamePrefab;
        
        public override IEnumerable<IGamePrefab> GetGamePrefabs()
        {
            yield return gamePrefab;
        }

        public override void InitGamePrefabs(IEnumerable<IGamePrefab> gamePrefabs)
        {
            gamePrefab = gamePrefabs.First();
        }

        #region Interface Implementation

        string INameOwner.name
        {
            get
            {
                if (gamePrefab == null)
                {
                    if (this != null)
                    {
                        return name;
                    }

                    return $"Null {nameof(GamePrefabSingleWrapper)}";
                }

                return gamePrefab.name;
            }
        }

        #endregion
    }
}