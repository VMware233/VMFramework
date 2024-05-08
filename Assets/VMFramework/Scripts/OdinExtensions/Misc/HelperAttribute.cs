using System;
using UnityEngine;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine.WSA;

#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]

    public class HelperAttribute : Attribute
    {
        public readonly string URL;

        public HelperAttribute(string URL)
        {
            this.URL = URL;
        }
    }

#if UNITY_EDITOR

    public class HelperAttributeDrawer : OdinAttributeDrawer<HelperAttribute>
    {
        private ValueResolver<string> urlResolver;

        protected override void Initialize()
        {
            urlResolver = ValueResolver.GetForString(Property, Attribute.URL);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (urlResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(urlResolver.ErrorMessage);
            }

            var url = urlResolver.GetValue();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            CallNextDrawer(label);
            GUILayout.EndVertical();
            GUIHelper.PushGUIEnabled(true);

            var rect = EditorGUILayout.GetControlRect(
                false, EditorGUIUtility.singleLineHeight,
                GUILayout.Width(12f)).AlignCenter(12f);

            if (SirenixEditorGUI.SDFIconButton(rect, url,
                    SdfIconType.QuestionCircleFill, style: SirenixGUIStyles.Label))
            {
                Launcher.LaunchUri(url, false);
            }



            GUIHelper.PopGUIEnabled();
            GUILayout.EndHorizontal();
        }
    }

#endif
}
