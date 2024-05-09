using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [PreviewComposite]
    public abstract partial class GameItem : IGameItem
    {
        #region Properties & Fields

        [LabelText("GamePrefab")]
        [ShowInInspector]
        protected IGameTypedGamePrefab origin { get; private set; }
        
        [LabelText("ID")]
        [ShowInInspector, DisplayAsString]
        public string id => origin.id;

        public string name => origin.name;
        
        [ShowInInspector]
        public bool isDebugging => origin.isDebugging;
        
        [LabelText("游戏种类")]
        [ShowInInspector]
        public IReadOnlyGameTypeSet gameTypeSet => origin.gameTypeSet;
    
        [LabelText("唯一游戏种类")]
        [ShowInInspector]
        public GameType uniqueGameType => origin.uniqueGameType;

        #endregion
    
        #region Interface Implementation

        IGameTypedGamePrefab IGameItem.origin
        {
            get => origin;
            set => origin = value;
        }
        
        void IGameItem.OnCreate()
        {
            OnCreate();
        }

        void IGameItem.OnClone(IGameItem other)
        {
            OnClone(other);
        }

        #endregion

        #region Clone

        protected virtual void OnClone(IGameItem other)
        {
            
        }

        #endregion

        #region Create

        protected virtual void OnCreate()
        {
            
        }

        #endregion

        #region To String

        protected virtual IEnumerable<(string propertyID, string propertyContent)> OnGetStringProperties()
        {
            yield break;
        }

        public override string ToString()
        {
            var extraString = OnGetStringProperties()
                .Select(property => property.propertyID + ":" + property.propertyContent)
                .Join(", ");

            return $"[{GetType()}:id:{id},{extraString}]";
        }

        #endregion
    }
}
