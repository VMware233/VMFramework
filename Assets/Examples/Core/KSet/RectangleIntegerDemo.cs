using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Examples
{
    public class RectangleIntegerDemo : MonoBehaviour
    {
        void Start()
        {
            // 创建一个[0, 2]x[0, 3]的整数矩形，用尺寸初始化
            // 这里乘号是笛卡尔积，相当于一个从坐标(0, 0)到(2, 3)的矩形
            var a = new RectangleInteger(new Vector2Int(3, 4));

            // 创建一个[2, 3]x[5, 6]的整数矩形，用最小值和最大值初始化
            var b = new RectangleInteger(new Vector2Int(2, 3), new Vector2Int(5, 6));

            // 复制一个整数矩形
            var c = new RectangleInteger(a);

            // 用RectangleInteger.unit快速创建一个[0, 1]x[0, 1]的整数矩形
            var d = RectangleInteger.unit;

            // [0, 2]x[0, 3]与(3, 4)相加，得到一个[3, 6]x[3, 7]的整数矩形
            var e = a + new Vector2Int(3, 4);

            // [3, 6]x[3, 7]加负号，得到一个[-6, -3]x[-7, -3]的整数矩形
            // 与相对于坐标原点作镜像一样
            var f = -e;

            // 创建一个[0, 2]x[0, 3]的整数矩形，即从坐标(0, 0)到(2, 3)的矩形
            var rangeX = RangeInteger.unit * 2;
            var rangeY = RangeInteger.unit * 3;
            var rectangle = rangeX * rangeY;

            // 获取[0, 2]x[0, 3]这个矩形上所有边界上的整数点
            foreach (var point in rectangle.GetAllBoundaryPoints())
            {
                Debug.Log(point);
            }

            // 获取[0, 2]x[0, 3]这个矩形内的所有整数点（包括边界）
            foreach (var point in rectangle.GetAllPoints())
            {
                Debug.Log(point);
            }

            // 从[0, 2]x[0, 3]这个矩形内随机取5个不同的随机点（包括边界）
            foreach (var point in rectangle.GetRandomUniquePoints(5))
            {
                Debug.Log(point);
            }
        }
    }
}
