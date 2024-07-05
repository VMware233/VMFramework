#if UNITY_EDITOR && ODIN_INSPECTOR

using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    internal sealed class GamePrefabContextMenuDrawer : OdinValueDrawer<GamePrefab>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            if (ValueEntry.SmartValue.gameItemType != null)
            {
                genericMenu.AddSeparator();
                
                genericMenu.AddItem($"Open {nameof(GameItem)} Script", () =>
                {
                    ValueEntry.SmartValue.gameItemType.OpenScriptOfType();
                });
            }
        }
    }
}

#endif