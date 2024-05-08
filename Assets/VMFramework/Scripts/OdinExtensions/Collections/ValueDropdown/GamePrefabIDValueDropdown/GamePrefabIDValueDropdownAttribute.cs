using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class GamePrefabIDValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] GamePrefabTypes;

        public bool FilterByGameItemType;
        
        public Type GameItemType;

        public GamePrefabIDValueDropdownAttribute(bool filterByGameItemType, Type type)
        {
            FilterByGameItemType = filterByGameItemType;
            if (filterByGameItemType)
            {
                GameItemType = type;
            }
            else
            {
                GamePrefabTypes = new[] { type };
            }
        }

        public GamePrefabIDValueDropdownAttribute(params Type[] gamePrefabTypes)
        {
            FilterByGameItemType = false;
            GamePrefabTypes = gamePrefabTypes;
        }

        public GamePrefabIDValueDropdownAttribute()
        {
            FilterByGameItemType = false;
            GamePrefabTypes = new[] { typeof(IGamePrefab) };
        }
    }
}