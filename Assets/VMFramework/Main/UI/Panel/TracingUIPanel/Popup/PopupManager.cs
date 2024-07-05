using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class PopupManager : ManagerBehaviour<PopupManager>
    {
        #region Popup Text

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPopupTextController PopupText(
            [UIPresetID(typeof(IPopupTextPreset))]
            string damagePopupID, TracingConfig config, SimpleText text, Color? textColor = null)
        {
            var popup = TracingUIManager.OpenOn<IPopupTextController>(damagePopupID, config);

            popup.text = text.text;

            if (textColor.HasValue)
            {
                popup.textColor = textColor.Value;
            }
            
            return popup;
        }

        #endregion
    }
}
