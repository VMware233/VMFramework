#if FISHNET && UNITY_EDITOR
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        [Button]
        private static void SerializeContainerItemTest(
            [GamePrefabIDValueDropdown(true, typeof(ContainerItem))] string containerItemID)
        {
            var containerItem = IGameItem.Create<ContainerItem>(containerItemID);
            containerItem.AssertIsNotNull(nameof(containerItem));
            instance.SerializeContainerItemTestRPC(containerItem);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SerializeContainerItemTestRPC(ContainerItem containerItem,
            NetworkConnection connection = null)
        {
            Debug.LogWarning($"Serializing container item: {containerItem}");
        }
    }
}
#endif