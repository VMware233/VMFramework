#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    internal sealed class GamePrefabWrapperContextMenuDrawer : OdinValueDrawer<GamePrefabWrapper>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = ValueEntry.SmartValue;
        
            if (value.GetGamePrefabs().Any() == false)
            {
                return;
            }
            
            var firstPrefab = value.GetGamePrefabs().First();

            if (firstPrefab == null)
            {
                return;
            }
            
            genericMenu.AddSeparator();
                
            genericMenu.AddItem($"Open {nameof(GamePrefab)} Script", () =>
            {
                firstPrefab.GetType().OpenScriptOfType();
            });
                
            if (firstPrefab.gameItemType != null)
            {
                genericMenu.AddItem($"Open {nameof(GameItem)} Script", () =>
                {
                    firstPrefab.gameItemType.OpenScriptOfType();
                });
            }
        }
    }
}
#endif