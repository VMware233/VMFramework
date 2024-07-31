// ReSharper disable InconsistentNaming
namespace VMFramework.GameLogicArchitecture
{
    public interface INameOwner
    {
        /// <summary>
        /// Tip: the name of this property is "name" to match the <see cref="UnityEngine.GameObject"/>'s 
        /// property "name"
        /// </summary>
        public string name { get; }
    }
}
