using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public interface ILocalizedNameOwner
    {
        public IReadOnlyLocalizedStringReference NameReference { get; }
    }
}
