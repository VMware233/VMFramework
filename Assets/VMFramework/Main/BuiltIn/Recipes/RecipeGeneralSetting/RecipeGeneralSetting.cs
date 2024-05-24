using System;
using Newtonsoft.Json;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipe
{
    public sealed partial class RecipeGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type baseGamePrefabType => typeof(Recipe);

        #endregion
    }
}
