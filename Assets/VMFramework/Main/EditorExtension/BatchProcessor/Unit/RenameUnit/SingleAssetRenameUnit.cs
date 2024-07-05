#if UNITY_EDITOR
using System.Collections.Generic;

namespace VMFramework.Editor.BatchProcessor
{
    [UnitSettings(UnitPriority.Super)]
    public sealed class SingleAssetRenameUnit : SingleButtonRenameAssetUnit
    {
        protected override string processButtonName => "Rename";

        public string newName;
        
        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Count == 1;
        }

        protected override string ProcessAssetName(string oldName)
        {
            return newName;
        }
    }
}
#endif