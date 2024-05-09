using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public sealed partial class ExtendedRuleTileGeneralSetting : GamePrefabGeneralSetting
    {
        public override string prefabName => "Extended Rule Tile";

        public override Type baseGamePrefabType => typeof(ExtendedRuleTile);

        [LabelText("默认的特定瓦片")]
        [GamePrefabIDValueDropdown(typeof(ExtendedRuleTile))]
        [GUIColor(0.7f, 0.7f, 1)]
        public HashSet<string> defaultSpecificTiles = new();

        [LabelText("默认的非特定瓦片")]
        [GamePrefabIDValueDropdown(typeof(ExtendedRuleTile))]
        [GUIColor(0.5f, 1f, 1)]
        public HashSet<string> defaultNotSpecificTiles = new();
    }
}
