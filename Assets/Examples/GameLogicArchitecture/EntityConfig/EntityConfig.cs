using System;
using VMFramework.GameLogicArchitecture;


namespace VMFramework.Examples 
{
    public partial class EntityConfig : LocalizedGameTypedGamePrefab, IEntityConfig
    {
        protected override string IDSuffix => "entity";
        
        public override Type GameItemType => typeof(Entity);
    }
}