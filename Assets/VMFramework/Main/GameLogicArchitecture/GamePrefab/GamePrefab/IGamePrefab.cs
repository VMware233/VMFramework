using System;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGamePrefab : IIDOwner, INameOwner, IInitializer, ICheckableConfig
    {
        public const string NULL_ID = "null";
        
        public new string id { get; set; }

        string IIDOwner<string>.id => id;

        public bool isActive { get; set; }
        
        public bool isDebugging { get; set; }
        
        public Type gameItemType { get; }
        
        public event Action<IGamePrefab, string, string> OnIDChangedEvent;
    }
}
