#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.Editor;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile : IGameEditorMenuTreeNode
    {
        Sprite IGameEditorMenuTreeNode.spriteIcon
        {
            get
            {
                var defaultIcon = GetIconFromSpriteLayers(defaultSpriteLayers);

                if (defaultIcon != null)
                {
                    return defaultIcon;
                }
                
                if (ruleSet.IsNullOrEmpty())
                {
                    return null;
                }
                    
                var firstRule = ruleSet[0];
                    
                return GetIconFromSpriteLayers(firstRule.layers);
            }
        }

        private Sprite GetIconFromSpriteLayers(IList<SpriteLayer> spriteLayers)
        {
            if (spriteLayers.Count == 0)
            {
                return null;
            }
            
            return spriteLayers[0].sprite;
        }
    }
}
#endif