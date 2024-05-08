using System;
using System.Diagnostics;
using Sirenix.Utilities;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    /// <summary>
    ///     Displays a placeholder text inside a text field if empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class PlaceholderAttribute : Attribute
    {
        /// <summary>
        ///     Placeholder text shown in the string field.
        /// </summary>
        public string Placeholder;

        /// <summary>
        ///     Text is bounded to the right of the string field.
        /// </summary>
        public bool RightSide;

        /// <summary>
        ///     Always show the placeholder even if the text is entered in the string field.
        /// </summary>
        public bool AlwaysShow;

        /// <inheritdoc cref="PlaceholderAttribute" />
        /// <param name="placeholder">Placeholder text shown in the string field.</param>
        /// <param name="rightSide">Text is bounded to the right of the string field.</param>
        /// <param name="alwaysShow">Always show the placeholder even if text is entered in the string field.</param>
        public PlaceholderAttribute(string placeholder, bool rightSide = false, bool alwaysShow = false)
        {
            Placeholder = placeholder;
            RightSide = rightSide;
            AlwaysShow = alwaysShow;
        }
    }

#if UNITY_EDITOR

    [AllowGUIEnabledForReadonly]
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class PlaceholderTextAttributeDrawer : OdinAttributeDrawer<PlaceholderAttribute, string>
    {
        private static GUIStyle _rightAlignedGreyMiniLabel;

        private static GUIStyle RightAlignedGreyMiniLabel =>
            _rightAlignedGreyMiniLabel ??= new GUIStyle(SirenixGUIStyles.RightAlignedGreyMiniLabel)
            {
                alignment = TextAnchor.UpperRight,
                padding = new RectOffset(2, 2, 3, 2)
            };

        private static GUIStyle _leftAlignedGreyMiniLabel;

        private static GUIStyle LeftAlignedGreyMiniLabel =>
            _leftAlignedGreyMiniLabel ??= new GUIStyle(SirenixGUIStyles.LeftAlignedGreyMiniLabel)
            {
                alignment = TextAnchor.UpperLeft,
                padding = new RectOffset(2, 2, 3, 2)
            };

        private ValueResolver<string> labelResolver;

        protected override void Initialize()
        {
            labelResolver = ValueResolver.GetForString(Property, Attribute.Placeholder);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            labelResolver.DrawError();

            CallNextDrawer(label);

            if (ValueEntry.SmartValue.IsNullOrWhitespace() || Attribute.AlwaysShow)
            {
                GUIHelper.PushGUIEnabled(true);
                if (Attribute.RightSide)
                {
                    GUI.Label(GUILayoutUtility.GetLastRect().HorizontalPadding(0.0f, 4.0f),
                        labelResolver.GetValue(), RightAlignedGreyMiniLabel);
                }
                else
                {
                    GUI.Label(
                        GUILayoutUtility.GetLastRect()
                            .HorizontalPadding(4.0f + (label == null ? 0 : GUIHelper.BetterLabelWidth), 0.0f),
                        labelResolver.GetValue(), LeftAlignedGreyMiniLabel);
                }

                GUIHelper.PopGUIEnabled();
            }
        }
    }

#endif
}