using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.Examples
{
    public sealed partial class PlayerGeneralSetting : GeneralSetting
    {
        [field: Layer]
        [field: SerializeField]
        public int playerLayer { get; private set; }
        
        public IVectorChooserConfig<int> defaultAttack;

        protected override void OnInit()
        {
            base.OnInit();
            
            // Write your initialization code here.
        }

        protected override IEnumerable<InitializationAction> GetInitializationActions()
        {
            return base.GetInitializationActions().Concat(new(InitializationOrder.PostInit, OnPostInit, this));
        }

        private async void OnPostInit(Action onDone)
        {
            // Write your post-initialization code here.
            
            onDone();
        }
    }
}