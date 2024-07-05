using System;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class NotificationUIPanelPreset : UIToolkitPanelPreset
    {
        protected const string NOTIFICATION_CATEGORY = "通知面板";

        public override Type controllerType => typeof(NotificationUIPanelController);

        [LabelText("通知容器"), TabGroup(TAB_GROUP_NAME, NOTIFICATION_CATEGORY)]
        [VisualElementName]
        public string notificationContainer;
    }
}
