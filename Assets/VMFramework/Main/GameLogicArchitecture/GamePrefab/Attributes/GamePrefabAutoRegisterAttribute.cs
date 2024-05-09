using System;

namespace VMFramework.GameLogicArchitecture
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class GamePrefabAutoRegisterAttribute : Attribute
    {
        public readonly string ID;
        
        public GamePrefabAutoRegisterAttribute(string id)
        {
            ID = id;
        }
    }

    public interface IGamePrefabAutoRegisterProvider
    {
        public void OnGamePrefabAutoRegister();
    }
}