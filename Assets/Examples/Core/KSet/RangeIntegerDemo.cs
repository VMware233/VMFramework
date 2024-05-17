using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Examples
{
    public class RangeIntegerDemo : MonoBehaviour
    {
        void Start()
        {
            // 创建一个[0, 2]的整数范围，用长度初始化
            var a = new RangeInteger(3);

            // 创建一个[3, 7]的整数范围，用Vector2Int的x和y分别作为最小值和最大值初始化
            var b = new RangeInteger(new Vector2Int(3, 7));

            // 从b复制一个整数范围
            var c = new RangeInteger(b);

            // 用RangeInteger.unit快速创建一个[0, 1]的整数范围
            var d = RangeInteger.unit;

            // 与3相加，得到一个[3, 4]的整数范围
            var e = d + 3;

            // 与3相乘，得到一个[0, 3]的整数范围
            var f = d * 3;

            // 创建一个[3, 7]的整数范围，用最小值和最大值初始化
            var rangeInt = new RangeInteger(3, 7);

            // 获取[3, 7]内的5个随机整数
            foreach (var point in rangeInt.GetRandomPoints(5))
            {
                Debug.Log(point);
            }

            // 获取[3, 7]内的5个不同的随机整数
            foreach (var point in rangeInt.GetRandomUniquePoints(5))
            {
                Debug.Log(point);
            }

            // 获取[3, 7]内的所有整数
            foreach (var point in rangeInt.GetAllPoints())
            {
                Debug.Log(point);
            }

            var num = 2;

            // 判断2是否在[3, 7]内
            if (rangeInt.Contains(num))
            {
                Debug.Log($"{num}在{rangeInt}内");
            }
            else
            {
                Debug.Log($"{num}不在{rangeInt}内");
            }

            // 获取2相对于[3, 7]的位置，此处输出-1
            var relativeNum = rangeInt.GetRelativePos(num);

            // 确保2比[3, 7]的最小值大，此处输出3
            var clampedMinNum = rangeInt.ClampMin(num);

            // 确保2比[3, 7]的最大值小，此处输出2
            var clampedMaxNum = rangeInt.ClampMax(num);

            // 获取[3, 7]的中心点，此处输出5
            var pivot = rangeInt.pivot;
        }
    }
}
