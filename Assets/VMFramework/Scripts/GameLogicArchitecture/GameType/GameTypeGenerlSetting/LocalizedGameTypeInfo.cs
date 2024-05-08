using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        private partial class LocalizedGameTypeInfo : GameTypeInfo, INameOwner, ILocalizedNameOwner
        {
            [LabelText("名称")]
            [PropertyOrder(-5000)]
            public LocalizedStringReference name = new();

            #region GUI

            protected override void OnInspectorInit()
            {
                base.OnInspectorInit();
                
                name ??= new();
            }

            #endregion

            #region Interface Implementation

            string INameOwner.name => name;

            IReadOnlyLocalizedStringReference ILocalizedNameOwner.nameReference => name;

            #endregion
        }
    }
}