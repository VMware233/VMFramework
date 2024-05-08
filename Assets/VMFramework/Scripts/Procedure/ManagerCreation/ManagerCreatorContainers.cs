using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure
{
    public static class ManagerCreatorContainers
    {
        public static Transform managerContainer { get; private set; }
        
        private static readonly Dictionary<ManagerType, Transform> _managerTypeContainers = new();

        public static IReadOnlyDictionary<ManagerType, Transform> managerTypeContainers =>
            _managerTypeContainers;
        
        public static void Init()
        {
            _managerTypeContainers.Clear();
            
            var managerContainerName = GameCoreSettingBase.managerCreationGeneralSetting.managerContainerName;

            managerContainerName.AssertIsNotNullOrWhiteSpace(nameof(managerContainerName));

            var managerContainerGameObject = managerContainerName.FindOrCreateGameObject();
            
            managerContainerGameObject.AssertIsNotNull(nameof(managerContainerGameObject));
            
            managerContainer = managerContainerGameObject.transform;
            
            managerContainer.SetAsFirstSibling();
            
            foreach (var managerType in Enum.GetValues(typeof(ManagerType)).Cast<ManagerType>())
            {
                var managerTypeContainerName = managerType.ToString();

                var managerTypeContainerGameObject =
                    managerTypeContainerName.FindOrCreateGameObject(managerContainer);

                managerTypeContainerGameObject.AssertIsNotNull(nameof(managerTypeContainerGameObject));
                
                _managerTypeContainers.Add(managerType, managerTypeContainerGameObject.transform);
            }

            foreach (var managerType in Enum.GetValues(typeof(ManagerType)).Cast<ManagerType>())
            {
                _managerTypeContainers[managerType].SetAsLastSibling();
            }
        }
        
        public static Transform GetManagerTypeContainer(ManagerType managerType)
        {
            return _managerTypeContainers[managerType];
        }
        
        public static IEnumerable<Transform> GetAllManagerTypeContainers()
        {
            return _managerTypeContainers.Values;
        }
    }
}