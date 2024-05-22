using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure
{
    public sealed partial class ManagerCreationGeneralSetting : GeneralSetting
    {
        #region Categories

        private const string MANAGER_CATEGORY = "管理器";

        #endregion

        [LabelText("为Abstract的管理器类别"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [ShowInInspector]
        private static IReadOnlyCollection<Type> abstractManagerTypes =>
            ManagerCreator.abstractManagerTypes;

        [LabelText("为Interface的管理器类别"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [ShowInInspector]
        private static IReadOnlyCollection<Type> interfaceManagerTypes =>
            ManagerCreator.interfaceManagerTypes;
        
        [LabelText("管理器类别"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [Searchable]
        [ShowInInspector, EnableGUI]
        private static IReadOnlyCollection<Type> managerTypes => ManagerCreator.managerTypes;

        [LabelText("管理器容器"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [ShowInInspector]
        public Transform managerContainer => ManagerCreatorContainers.managerContainer;
        
        [LabelText("管理器类型容器"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [ShowInInspector]
        public IReadOnlyDictionary<string, Transform> managerTypeContainers =>
            ManagerCreatorContainers.managerTypeContainers;

        [field: LabelText("管理器容器名称"), TabGroup(TAB_GROUP_NAME, MANAGER_CATEGORY)]
        [field: SerializeField]
        public string managerContainerName { get; private set; } = "^Core";
    }
}