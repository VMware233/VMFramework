using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public sealed partial class GamePrefabIDSetter<TGamePrefab> : 
        ObjectChooser<GamePrefabIDSetter<TGamePrefab>.GamePrefabID>
        where TGamePrefab : IGamePrefab
    {
        public struct GamePrefabID : ICloneable
        {
#if UNITY_EDITOR
            [ValueDropdown(nameof(GetNameList))]
            [HideLabel]
            [IsNotNullOrEmpty]
#endif
            public string id;

            #region GUI

#if UNITY_EDITOR
            private IEnumerable<ValueDropdownItem> GetNameList()
            {
                return GamePrefabManager.GetGamePrefabNameListByType(typeof(TGamePrefab));
            }
#endif

            #endregion

            #region To String

            public override string ToString()
            {
                return GamePrefabManager.GetGamePrefab<TGamePrefab>(id)?.name;
            }

            #endregion

            #region Cloneable

            public object Clone()
            {
                return new GamePrefabID() { id = id };
            }

            #endregion
        }

        #region Operators

        public static implicit operator string(GamePrefabIDSetter<TGamePrefab> setter)
        {
            return setter.GetValue().id;
        }

        public static implicit operator GamePrefabIDSetter<TGamePrefab>(string id)
        {
            return new()
            {
                value = new()
                {
                    id = id
                }
            };
        }

        #endregion
    }
}