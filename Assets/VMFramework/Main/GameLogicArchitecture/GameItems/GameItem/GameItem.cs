using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Pools;
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
        protected IGameTypedGamePrefab GamePrefab { get; private set; }

        [ShowInInspector, DisplayAsString]
        public string id => GamePrefab.id;

        public string name => GamePrefab.name;

        [ShowInInspector]
        public bool isDebugging => GamePrefab.IsDebugging;

        [ShowInInspector]
        public IReadOnlyGameTypeSet GameTypeSet => GamePrefab.GameTypeSet;

        [ShowInInspector]
        public GameType UniqueGameType => GamePrefab.UniqueGameType;

        [ShowInInspector]
        public bool IsDestroyed { get; private set; } = false;

        #endregion

        #region Clone

        public virtual void CloneTo(IGameItem other)
        {

        }

        #endregion

        #region Pool Events

        void IPoolItem.OnGet()
        {
            OnGet();
        }

        void ICreatablePoolItem<string>.OnCreate(string argument)
        {
            GamePrefab = GamePrefabManager.GetGamePrefabStrictly<IGameTypedGamePrefab>(id);

            OnCreate();
            OnGet();
        }

        void IPoolItem.OnReturn()
        {
            OnReturn();
        }

        void IPoolItem.OnClear()
        {
            OnReturn();
            OnClear();
        }

        protected virtual void OnGet()
        {

        }

        protected virtual void OnCreate()
        {

        }

        protected virtual void OnReturn()
        {

        }

        protected virtual void OnClear()
        {

        }

        #endregion

        #region To String

        protected virtual void OnGetStringProperties(
            ICollection<(string propertyID, string propertyContent)> collection)
        {

        }

        public override string ToString()
        {
            var list = ListPool<(string propertyID, string propertyContent)>.Shared.Get();
            OnGetStringProperties(list);

            var extraString = list.Select(property => property.propertyID + ":" + property.propertyContent).Join(", ");
            list.ReturnToPool();

            return $"[{GetType()}:id:{id},{extraString}]";
        }

        #endregion
    }
}