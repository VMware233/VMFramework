using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class GameItemEvents
    {
        private static readonly HashSet<IGameItemEventsReceiver> receivers = new();
        
        public static event Action<IGameItem> OnGameItemCreated
        {
            add
            {
                foreach (var receiver in receivers)
                {
                    receiver.OnGameItemCreated += value;
                }
            }
            remove
            {
                foreach (var receiver in receivers)
                {
                    receiver.OnGameItemCreated -= value;
                }
            }
        }
        
        public static event Action<IGameItem> OnGameItemDestroyed
        {
            add
            {
                foreach (var receiver in receivers)
                {
                    receiver.OnGameItemDestroyed += value;
                }
            }
            remove
            {
                foreach (var receiver in receivers)
                {
                    receiver.OnGameItemDestroyed -= value;
                }
            }
        }

        static GameItemEvents()
        {
            foreach (var type in typeof(IGameItemEventsReceiver).GetDerivedInstantiableClasses(false))
            {
                var receiver = (IGameItemEventsReceiver)Activator.CreateInstance(type);
                receivers.Add(receiver);
            }
        }
    }
}