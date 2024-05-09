#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public class PathContextMenuDrawer : OdinValueDrawer<string>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = property.ValueEntry.WeakSmartValue;

            if (value is not string str)
            {
                return;
            }

            bool isPath = false;
            string path = str.TrimStart('/', '\\');

            if (str.StartsWith("Assets/"))
            {
                isPath = true;
            }
            else if (str.StartsWith("Resources/"))
            {
                path = "Assets/" + path;
                isPath = true;
            }

            if (isPath == false)
            {
                return;
            }
            
            path = IOUtility.projectFolderPath + path;
            path = path.GetDirectoryPath();

            if (path.ExistsDirectory() == false)
            {
                return;
            }
            
            genericMenu.AddItem(new GUIContent("打开文件夹"), false, () =>
            {
                path.OpenDirectory(false);
            });
        }
    }
}
#endif