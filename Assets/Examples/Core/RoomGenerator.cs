﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Examples
{
    public sealed class RoomGenerationInfo : ITreeNode<RoomGenerationInfo>
    {
        // 父房间生成信息
        // The Room Generation Info of the parent room.
        public RoomGenerationInfo parentRoomInfo { get; init; }

        // 房间的入口信息，表示父房间的方向
        // The entry information of the room, which represents the direction of the parent room.
        public FourTypesDirection enterDirection { get; init; }

        // 不同房间的出口对应的相邻房间生成信息
        // FourTypesDirection2D是框架内自带的枚举类型，表示2D空间的四个方向：上、下、左、右
        // The Room Generation Info of the adjacent room corresponding to the exit of different rooms.
        // FourTypesDirection2D is an enumeration type built into the framework,
        // representing the four directions of 2D space: up, down, left, and right.
        private readonly Dictionary<FourTypesDirection, RoomGenerationInfo> _childrenRoomDict = new();

        // 这里可能会有一些生成数据
        // 比如坐标、房间深度啥的
        // The data for generating the room, such as coordinates and room depth.

        // 构造函数，初始化入口方向和父房间生成信息
        // The constructor initializes the entry direction and parent room generation information.
        public RoomGenerationInfo(FourTypesDirection enterDirection, RoomGenerationInfo parentRoomInfo)
        {
            this.enterDirection = enterDirection;
            this.parentRoomInfo = parentRoomInfo;

            parentRoomInfo.AddChildRoom(enterDirection.Reversed(), this);
        }

        // 构造函数，这样构造表示是根房间，或者说是第一个房间
        // The constructor, which is used to construct the root room or the first room,
        // initializes the entry direction and parent room generation information.
        public RoomGenerationInfo() : this(FourTypesDirection.None, null)
        {
        }

        // 添加子房间，这里的子房间是指相邻的房间，出口方向不能与入口方向相同
        // 在生成地牢房间结构时，使用此函数来添加子房间
        // The function is used to add a child room, which is adjacent to the current room,
        // and the exit direction cannot be the same as the entry direction.
        // This function is used to add child rooms when generating the dungeon room structure.
        private void AddChildRoom(FourTypesDirection direction, RoomGenerationInfo roomGenerationInfo)
        {
            if (direction == enterDirection)
                // 不能在入口方向添加子房间
                throw new Exception("Cannot add a child room in the same direction as the entry direction.");

            _childrenRoomDict[direction] = roomGenerationInfo;
        }

        #region ITreeNode

        RoomGenerationInfo IParentProvider<RoomGenerationInfo>.GetParent() =>
            parentRoomInfo;

        [ShowInInspector]
        IEnumerable<RoomGenerationInfo> IChildrenProvider<RoomGenerationInfo>.GetChildren() =>
            _childrenRoomDict.Values;

        #endregion
    }

    public sealed class RoomGeneratorTest : MonoBehaviour
    {
        private void Start()
        {
            var info1 = new RoomGenerationInfo();
            var info2 = new RoomGenerationInfo(FourTypesDirection.Down, info1);
            var info3 = new RoomGenerationInfo(FourTypesDirection.Left, info1);
            var info4 = new RoomGenerationInfo(FourTypesDirection.Right, info2);

            // 获取根房间，这里root就是info1
            var root = info4.GetRoot();

            // 判断info4是否是info2的子节点，这里返回true
            var hasParentInfo2 = info4.HasParent(info2, false);

            foreach (var info in info4.TraverseToRoot(true))
            {
                // 遍历info4到根节点的路径，这里会依次遍历info4、info2、info1
                // TraverseToRoot方法的参数表示是否包含自身节点
                // 如果为false，则不包含自身节点，只会遍历info2、info1
            }

            foreach (var info in info1.PreorderTraverse(true))
            {
                // 前序遍历info1的子树，这里会依次遍历info1、info2、info4、info3
                // PreorderTraverse方法的参数表示是否包含自身节点，与TraverseToRoot方法类似
            }

            foreach (var info in info1.PostorderTraverse(true))
            {
                // 后序遍历info1的子树，这里会依次遍历info4、info2、info3、info1
                // PostorderTraverse方法的参数表示是否包含自身节点，与TraverseToRoot方法类似
            }

            foreach (var info in info1.LevelOrderTraverse(true))
            {
                // 层序遍历info1的子树，这里会依次遍历info1、info2、info3、info4
                // LevelOrderTraverse方法的参数表示是否包含自身节点，与TraverseToRoot方法类似
            }

            // 获取info1的所有子节点，这里会返回info2、info3、info4
            // 如果想要包含info1，可以将参数设为true
            List<RoomGenerationInfo> allChildren = info1.PreorderTraverse(false).ToList();

            // 获取info1的所有叶子节点，这里会返回info3、info4
            List<RoomGenerationInfo> allLeaves = info1.GetAllLeaves(false).ToList();

            // 判断info4是否是info1的子节点，这里返回true
            var hasChildInfo4 = info1.HasChild(info4, false);


            transform.GetComponentInChildren<SpriteRenderer>();

            List<Transform> allParents = transform.TraverseToRoot(true, t => t.parent).ToList();

            // 起始坐标
            var startPoint = Vector2Int.zero;
            // 最大搜索次数，防止死循环
            int maxSearchCount = 100;
            // 这里GetFourDirectionsNearPoints是框架内自带的函数，
            // 用来获取一个Vector2Int坐标的上下左右四个方向相邻坐标，
            // 比如给这个函数传入(1,1)，会依次返回(1,2)、(1,0)、(0,1)、(2,1)
            foreach (var point in startPoint.LevelOrderTraverse(true, point => point.GetFourDirectionsNeighbors()))
            {
                Debug.Log(point);

                maxSearchCount--;
                if (maxSearchCount <= 0) break;
            }
        }
    }
}
