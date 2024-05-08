using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public class Vector3SetterCore : NumberOrVectorChooser<Vector3, CubeFloatConfig>
    {
        protected override string valueName => "向量";

        [LabelText("小数点后显示几位")]
        [MinValue(0)]
        [OnValueChanged("PreviewValue")]
        public int decimalPlaces = 1;

        #region To String

        protected override string ValueToString(Vector3 value)
        {
            return value.ToString(decimalPlaces);
        }

        #endregion

        #region Operator

        public static implicit operator Vector3SetterCore(Vector3 fixedVector)
        {
            return new()
            {
                isRandomValue = false,
                value = fixedVector
            };
        }

        public static implicit operator Vector3SetterCore(float num)
        {
            return new()
            {
                isRandomValue = false,
                value = new Vector3(num, num, num)
            };
        }

        #endregion
    }
}