using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public static class ManagerCreator
    {
        private static readonly HashSet<Type> _abstractManagerTypes = new();

        private static readonly HashSet<Type> _interfaceManagerTypes = new();

        private static readonly HashSet<Type> _managerTypes = new();

        private static readonly List<IManagerBehaviour> _managers = new();

        public static IReadOnlyCollection<Type> abstractManagerTypes => _abstractManagerTypes;

        public static IReadOnlyCollection<Type> interfaceManagerTypes => _interfaceManagerTypes;

        public static IReadOnlyCollection<Type> managerTypes => _managerTypes;
        
        public static IReadOnlyList<IManagerBehaviour> managers => _managers;

        public static void CreateManagers()
        {
            ManagerCreatorContainers.Init();

            var eventCoreContainer =
                ManagerCreatorContainers.GetOrCreateManagerTypeContainer(ManagerType.EventCore.ToString());

            eventCoreContainer.GetOrAddComponent<EventSystem>();
            eventCoreContainer.GetOrAddComponent<StandaloneInputModule>();

            _abstractManagerTypes.Clear();
            _interfaceManagerTypes.Clear();
            _managerTypes.Clear();
            _managers.Clear();

            var validManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();
            var invalidManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();

            foreach (var managerClassType in typeof(IManagerBehaviour).GetDerivedClasses(true, false))
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

                if (managerClassType.IsDerivedFrom<Component>(false, false) == false)
                {
                    Debug.LogWarning($"{managerClassType} is not derived from {nameof(Component)}");
                    continue;
                }

                var isParentOrChild = false;

                foreach (var (validManagerType, validProviderAttribute) in validManagerClassTypes.ToList())
                {
                    if (managerClassType.IsSubclassOf(validManagerType))
                    {
                        validManagerClassTypes.Remove(validManagerType);
                        invalidManagerClassTypes.Add(validManagerType, validProviderAttribute);
                        validManagerClassTypes.Add(managerClassType, providerAttribute);

                        isParentOrChild = true;
                        break;
                    }

                    if (validManagerType.IsSubclassOf(managerClassType))
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
                var managerTypeName = providerAttribute.ManagerTypeName;

                var container = ManagerCreatorContainers.GetOrCreateManagerTypeContainer(managerTypeName);

                container.RemoveFirstComponentImmediate(managerClassType);
            }

            foreach (var (managerClassType, providerAttribute) in validManagerClassTypes)
            {
                var managerTypeName = providerAttribute.ManagerTypeName;

                var container = ManagerCreatorContainers.GetOrCreateManagerTypeContainer(managerTypeName);
                
                var component = container.GetOrAddComponent(managerClassType);

                if (component is not IManagerBehaviour managerBehaviour)
                {
                    throw new Exception($"{managerClassType} does not implement {nameof(IManagerBehaviour)}");
                }
                
                _managers.Add(managerBehaviour);
                
                foreach (var otherContainer in ManagerCreatorContainers.GetOtherManagerTypeContainers(managerTypeName))
                {
                    otherContainer.RemoveAllComponentsImmediate(managerClassType);
                }
            }

            _managerTypes.UnionWith(validManagerClassTypes.Keys);
        }
    }
}