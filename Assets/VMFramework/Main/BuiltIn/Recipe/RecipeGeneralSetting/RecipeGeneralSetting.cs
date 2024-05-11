using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Recipe
{
    public sealed partial class RecipeGeneralSetting : GamePrefabGeneralSetting
    {
        public override Type baseGamePrefabType => typeof(Recipe);
    }
}
