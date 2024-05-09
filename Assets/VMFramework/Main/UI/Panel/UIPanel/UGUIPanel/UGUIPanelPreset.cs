using System;
using System.Collections;
using VMFramework.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.UI
{
    public class UGUIPanelPreset : UIPanelPreset
    {
        protected const string UGUI_PANEL_CATEGORY = "UGUI面板设置";

        public override Type controllerType => typeof(UGUIPanelController);

        [LabelText("UI预制体"), TabGroup(TAB_GROUP_NAME, UGUI_PANEL_CATEGORY)]
        [AssetsOnly]
        [Required]
        public GameObject prefab;

        public override void CheckSettings()
        {
            base.CheckSettings();

            prefab.AssertIsNotNull(nameof(prefab));
        }

        protected IEnumerable GetPrefabChildrenNames()
        {
            if (prefab == null)
            {
                return null;
            }

            return prefab.transform.GetAllChildrenNames(false);
        }

        protected IEnumerable GetPrefabChildrenNamesOfTextMeshProUGUI()
        {
            if (prefab == null)
            {
                return null;
            }

            return prefab.transform.GetAllChildrenNames<TextMeshProUGUI>(false);
        }

        protected IEnumerable GetPrefabChildrenNamesOfImage()
        {
            if (prefab == null)
            {
                return null;
            }

            return prefab.transform.GetAllChildrenNames<Image>(false);
        }
    }
}
