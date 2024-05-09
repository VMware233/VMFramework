namespace VMFramework.UI
{
    public partial class UIPanelPreset
    {
        public bool ShouldSerializecloseInputMappingID()
        {
            return enableCloseInputMapping;
        }

        public bool ShouldSerializetoggleInputMappingID()
        {
            return enableToggleInputMapping;
        }
    }
}