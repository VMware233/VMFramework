using System;
using System.Collections.Generic;
using EnumsNET;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [GameInitializerRegister(GameInitializationDoneProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class SpritePresetInitializer : IGameInitializer
    {
        IEnumerable<InitializationAction> IInitializer.GetInitializationActions()
        {
            yield return new(InitializationOrder.Init, OnInit, this);
        }

        private static void OnInit(Action onDone)
        {
            int count = 0;
            
            foreach (var spritePreset in GamePrefabManager.GetAllActiveGamePrefabs<SpritePreset>())
            {
                foreach (var flipType in spritePreset.preloadFlipType.GetFlags())
                {
                    SpriteManager.GetSprite(spritePreset.id, flipType);
                    
                    count++;
                }
            }

            if (count > 0)
            {
                Debug.Log($"Preloaded {count} sprites' flip types.");
            }
            
            onDone();
        }
    }
}