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

        [ShowInInspector]
        protected IGameTypedGamePrefab gamePrefab { get; private set; }
        
        [ShowInInspector, DisplayAsString]
        public string id => gamePrefab.id;

        public string name => gamePrefab.name;
        
        [ShowInInspector]
        public bool isDebugging => gamePrefab.isDebugging;
        
        [ShowInInspector]
        public IReadOnlyGameTypeSet gameTypeSet => gamePrefab.gameTypeSet;
    
        [ShowInInspector]
        public GameType uniqueGameType => gamePrefab.uniqueGameType;

        [ShowInInspector]
        public bool isDestroyed { get; private set; } = false;

        #endregion
    
        #region Interface Implementation

        IGameTypedGamePrefab IGameItem.origin
        {
            get => gamePrefab;
            set => gamePrefab = value;
        }

        void IGameItem.OnFirstCreatedGameItem()
        {
            OnFirstCreated();
        }

        void IGameItem.OnCreatedGameItem()
        {
            OnCreate();
        }

        void IGameItem.OnClone(IGameItem other)
        {
            OnClone(other);
        }

        void IDestructible.SetDestructible(bool isDestructible)
        {
            isDestroyed = isDestructible;
        }

        #endregion

        #region Clone

        protected virtual void OnClone(IGameItem other)
        {
            
        }

        #endregion

        #region Create & Destroy

        protected virtual void OnFirstCreated()
        {
            
        }

        protected virtual void OnCreate()
        {
            
        }

        void IGameItem.OnDestroyGameItem()
        {
            OnDestroy();
        }

        protected virtual void OnDestroy()
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
