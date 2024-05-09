using System;
using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.GlobalEvent
{
    [ManagerCreationProvider(ManagerType.EventCore)]
    public sealed class GlobalEventManager : ManagerBehaviour<GlobalEventManager>, IManagerBehaviour
    {
        [ShowInInspector]
        private static List<GlobalEventConfig> updateableGlobalEventConfigs;

        [ShowInInspector]
        private static bool initialized = false;

        private void Update()
        {
            if (initialized == false)
            {
                return;
            }

            foreach (var eventConfig in updateableGlobalEventConfigs)
            {
                eventConfig.Update();
            }
        }

        void IInitializer.OnInit(Action onDone)
        {
            updateableGlobalEventConfigs = GamePrefabManager.GetAllGamePrefabs<GlobalEventConfig>()
                .Where(config => config.requireUpdate).ToList();

            initialized = true;
            
            onDone();
        }

        public static void AddEvent(string mappingID, Action action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).action += action;
        }

        public static void AddEvent(string mappingID, Action<bool> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).boolAction += action;
        }

        public static void AddTriggerEvent(string mappingID, Action<bool> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).boolTriggerAction +=
                action;
        }

        public static void AddEvent(string mappingID, Action<float> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfFloatArg>(mappingID)
                .floatAction += action;
        }
        //
        // public static void AddEvent(string mappingID, Action<int> action)
        // {
        //     GlobalEventConfig
        //         .GetPrefabStrictly<InputEventConfigOfNumberArray>(mappingID)
        //         .intAction += action;
        // }

        public static void AddEvent(string mappingID, Action<Vector2> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfVector2Arg>(mappingID)
                .vector2Action += action;
        }

        public static void RemoveEvent(string mappingID, Action action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).action -= action;
        }

        public static void RemoveEvent(string mappingID, Action<bool> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).boolAction -= action;
        }

        public static void RemoveTriggerEvent(string mappingID, Action<bool> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).boolTriggerAction -=
                action;
        }

        public static void RemoveEvent(string mappingID, Action<float> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfFloatArg>(mappingID)
                .floatAction -= action;
        }

        public static void RemoveEvent(string mappingID, Action<Vector2> action)
        {
            GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfVector2Arg>(mappingID)
                .vector2Action -= action;
        }

        public static bool GetBoolValue(string mappingID)
        {
            return GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).boolValue;
        }

        public static float GetFloatValue(string mappingID)
        {
            return GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfFloatArg>(mappingID).floatValue;
        }

        public static Vector2 GetVector2State(string mappingID)
        {
            return GamePrefabManager.GetGamePrefabStrictly<InputEventConfigOfVector2Arg>(mappingID)
                .vector2Value;
        }

        public static void EnableEvent(string mappingID)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).EnableEvent();
        }

        public static void DisableEvent(string mappingID)
        {
            GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID).DisableEvent();
        }

        public static IEnumerable<string> GetInputMappingContent(string mappingID,
            KeyCodeUtility.KeyCodeToStringMode mode)
        {
            if (GamePrefabManager.GetGamePrefabStrictly<GlobalEventConfig>(mappingID) is not
                InputEventConfig inputMapping)
            {
                return Enumerable.Empty<string>();
            }

            return inputMapping.GetInputMappingContent(mode);
        }

        public static string GetInputMappingFirstContent(string mappingID,
            KeyCodeUtility.KeyCodeToStringMode mode)
        {
            return GetInputMappingContent(mappingID, mode).FirstOrDefault();
        }
    }
}