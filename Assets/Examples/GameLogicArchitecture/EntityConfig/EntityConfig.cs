using System;
using VMFramework.GameLogicArchitecture;


namespace VMFramework.Examples 
{
    public partial class EntityConfig : LocalizedGameTypedGamePrefab, IEntityConfig
    {
        protected override string idSuffix => "entity";
        
        public override Type gameItemType => typeof(Entity);
    }
}