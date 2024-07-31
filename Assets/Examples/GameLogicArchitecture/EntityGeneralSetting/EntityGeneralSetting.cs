using System;
using VMFramework.GameLogicArchitecture;


namespace VMFramework.Examples 
{
    public sealed partial class EntityGeneralSetting : GamePrefabGeneralSetting
    {
        #region Meta Data

        public override Type BaseGamePrefabType => typeof(IEntityConfig);

        #endregion

        
    }
}
