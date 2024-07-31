using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipes
{
    public sealed partial class RecipeGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type BaseGamePrefabType => typeof(Recipe);

        #endregion
    }
}
