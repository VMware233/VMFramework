#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public enum EditorIconType
    {
        Sprite,
        SdfIcon,
        Texture2D
    }
    
    public interface IGameEditorMenuTreeNode : INameOwner
    {
        public string folderPath => "";
        
        public EditorIconType iconType => EditorIconType.Sprite;
        
        public Sprite spriteIcon => null;
        
        public Texture2D texture2DIcon => null;
        
        public SdfIconType sdfIcon => SdfIconType.None;

        public string nodePath
        {
            get
            {
                if (folderPath.IsNullOrEmpty())
                {
                    return name;
                }

                return folderPath + "/" + name;
            }
        }
    }
}
#endif