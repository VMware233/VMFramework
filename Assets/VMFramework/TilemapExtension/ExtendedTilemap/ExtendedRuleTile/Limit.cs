using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public class Limit : BaseConfigClass
    {
        [HideLabel]
        [OnValueChanged(nameof(OnLimitTypeChanged))]
        [GUIColor(nameof(GetLimitColor))]
        public LimitType limitType;

        [LabelText("特定瓦片列表")]
        [ShowIf(nameof(showSpecificTilesList))]
        [ValueDropdown(
            "@GameCoreSettingBase.extendedRuleTileGeneralSetting.GetPrefabNameList()")]
        [IsNotNullOrEmpty]
        public List<string> specificTiles = new();

        #region GUI

        private bool showSpecificTilesList =>
            limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles;

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            specificTiles ??= new();
        }

        private Color GetLimitColor()
        {
            return limitType switch
            {
                LimitType.None => Color.white,
                LimitType.This => new(0.5f, 1, 0.5f, 1),
                LimitType.NotThis => new(1, 0.5f, 0.5f, 1),
                LimitType.SpecificTiles => new(0.7f, 0.7f, 1, 1),
                LimitType.NotSpecificTiles => new(0.5f, 1f, 1, 1),
                LimitType.IsEmpty => new(1, 1, 0.5f, 1),
                LimitType.NotEmpty => new(1, 0.5f, 1, 1),
                LimitType.ThisOrParent => new(0.6f, 0.9f, 0.8f, 1),
                LimitType.NotThisAndParent => new(0.9f, 0.7f, 0.5f, 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnLimitTypeChanged()
        {
            if (limitType == LimitType.SpecificTiles)
            {
                if (specificTiles == null || specificTiles.Count == 0)
                {
                    specificTiles = new();
                    specificTiles.AddRange(GameCoreSettingBase
                        .extendedRuleTileGeneralSetting.defaultSpecificTiles);
                }
            }

            if (limitType == LimitType.NotSpecificTiles)
            {
                if (specificTiles == null || specificTiles.Count == 0)
                {
                    specificTiles = new();
                    specificTiles.AddRange(GameCoreSettingBase
                        .extendedRuleTileGeneralSetting.defaultNotSpecificTiles);
                }
            }
        }

        #endregion

        public bool Equals(Limit other)
        {
            if (limitType != other.limitType)
            {
                return false;
            }

            if (limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles)
            {
                if (specificTiles.Count != other.specificTiles.Count)
                {
                    return false;
                }

                if (specificTiles.ToArray() != other.specificTiles.ToArray())
                {
                    return false;
                }
            }

            return true;
        }

        public void CopyFrom(Limit other)
        {
            limitType = other.limitType;

            if (limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles)
            {
                specificTiles = new();
                specificTiles.AddRange(other.specificTiles);
            }
        }

        public static implicit operator Limit(LimitType limitType)
        {
            return new()
            {
                limitType = limitType
            };
        }
    }
}