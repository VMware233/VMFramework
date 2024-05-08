using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure
{
    public static class ManagerCreator
    {
        private static readonly HashSet<Type> _abstractManagerTypes = new();

        private static readonly HashSet<Type> _interfaceManagerTypes = new();
        
        private static readonly HashSet<Type> _managerTypes = new();

        public static IReadOnlyCollection<Type> abstractManagerTypes => _abstractManagerTypes;

        public static IReadOnlyCollection<Type> interfaceManagerTypes => _interfaceManagerTypes;
        
        public static IReadOnlyCollection<Type> managerTypes => _managerTypes;

        public static void CreateManagers()
        {
            if (GameCoreSettingBase.managerCreationGeneralSetting == null)
            {
                Debug.LogWarning("No Manager Creation Settings found.");
                return;
            }
            
            ManagerCreatorContainers.Init();
            
            var eventCoreContainer = ManagerCreatorContainers.GetManagerTypeContainer(ManagerType.EventCore);

            eventCoreContainer.GetOrAddComponent<EventSystem>();
            eventCoreContainer.GetOrAddComponent<StandaloneInputModule>();

            _abstractManagerTypes.Clear();
            _interfaceManagerTypes.Clear();
            _managerTypes.Clear();

            var validManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();
            var invalidManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();

            foreach (var managerClassType in typeof(MonoBehaviour).GetDerivedClasses(includingSelf: true,
                         includingGenericDefinition: false))
            {
                if (managerClassType.TryGetAttribute<ManagerCreationProviderAttribute>(true,
                        out var providerAttribute) == false)
                {
                    continue;
                }

                if (managerClassType.IsAbstract)
                {
                    _abstractManagerTypes.Add(managerClassType);
                    continue;
                }

                if (managerClassType.IsInterface)
                {
                    _interfaceManagerTypes.Add(managerClassType);
                    continue;
                }

                bool isParentOrChild = false;

                foreach (var (validManagerClassType, validManagerProviderAttribute)in validManagerClassTypes
                             .ToList())
                {
                    if (managerClassType.IsSubclassOf(validManagerClassType))
                    {
                        validManagerClassTypes.Remove(validManagerClassType);
                        invalidManagerClassTypes.Add(validManagerClassType, validManagerProviderAttribute);
                        validManagerClassTypes.Add(managerClassType, providerAttribute);

                        isParentOrChild = true;
                        break;
                    }

                    if (validManagerClassType.IsSubclassOf(managerClassType))
                    {
                        invalidManagerClassTypes.Add(managerClassType, providerAttribute);

                        isParentOrChild = true;
                        break;
                    }
                }

                if (isParentOrChild == false)
                {
                    validManagerClassTypes.Add(managerClassType, providerAttribute);
                }
            }

            foreach (var (managerClassType, providerAttribute) in invalidManagerClassTypes)
            {
                var managerType = providerAttribute.ManagerType;

                var container = ManagerCreatorContainers.GetManagerTypeContainer(managerType);

                container.RemoveFirstComponentImmediate(managerClassType);
            }

            foreach (var (managerClassType, providerAttribute) in validManagerClassTypes)
            {
                var managerType = providerAttribute.ManagerType;

                var container = ManagerCreatorContainers.GetManagerTypeContainer(managerType);

                container.GetOrAddComponent(managerClassType);
            }

            _managerTypes.UnionWith(validManagerClassTypes.Keys);

            var managerCreationList = new List<IManagerCreationProvider>();

            if (GameCoreSettingBase.gameCoreSettingsFileBase != null)
            {
                if (GameCoreSettingBase.gameCoreSettingsFileBase is IManagerCreationProvider managerCreation)
                {
                    managerCreationList.Add(managerCreation);
                }
            }

            foreach (var generalSetting in GameCoreSettingBase.GetAllGeneralSettings())
            {
                if (generalSetting is IManagerCreationProvider managerCreation)
                {
                    managerCreationList.Add(managerCreation);
                }
            }

            foreach (var managerCreation in managerCreationList)
            {
                managerCreation.HandleManagerCreation();
            }
        }
    }
}
