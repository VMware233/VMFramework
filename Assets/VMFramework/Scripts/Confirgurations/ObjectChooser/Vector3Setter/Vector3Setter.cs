using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn)]
    [PreviewComposite]
    public sealed partial class Vector3Setter : BaseConfigClass, IValueSetter<Vector3>,
        INumberOrVectorValueSetter<Vector3>
    {
        [LabelText("仅作为二维向量")]
        [ToggleButtons("是", "否")]
        [JsonProperty]
        public bool asVector2D = false;

        [LabelText("向量所在平面")]
        [EnumToggleButtons]
        [ShowIf(nameof(asVector2D))]
        [JsonProperty]
        public PlaneType planeType = PlaneType.XY;

        [HideLabel]
        [HideIf(nameof(asVector2D))]
        [JsonProperty]
        public Vector3SetterCore setter3D = new();

        [HideLabel]
        [ShowIf(nameof(asVector2D))]
        [JsonProperty]
        public Vector2Setter setter2D = new();

        #region GUI

        protected override void OnInspectorInit()
        {
            setter3D ??= new();
            setter2D ??= new();

            base.OnInspectorInit();
        }

        #endregion

        #region JSON Serialization

        public bool ShouldSerializeplaneType()
        {
            return asVector2D;
        }

        public bool ShouldSerializesetter3D()
        {
            return asVector2D == false;
        }

        public bool ShouldSerializesetter2D()
        {
            return asVector2D;
        }

        #endregion

        public Vector3 GetValue()
        {
            return asVector2D
                ? setter2D.GetValue().InsertAs(0, planeType)
                : setter3D.GetValue();
        }

        public IEnumerable<Vector3> GetCurrentValues()
        {
            if (asVector2D)
            {
                return setter2D.GetCurrentValues().Select(v => v.InsertAs(0, planeType));
            }

            return setter3D.GetCurrentValues();
        }

        #region To String

        public override string ToString()
        {
            return asVector2D ? setter2D.ToString() : setter3D.ToString();
        }

        #endregion

        #region Operator

        public static implicit operator Vector3(Vector3Setter setter)
        {
            return setter.GetValue();
        }

        public static implicit operator Vector3Setter(Vector3 fixedVector)
        {
            return new()
            {
                asVector2D = false,
                planeType = PlaneType.XY,
                setter3D = fixedVector
            };
        }

        public static implicit operator Vector3Setter(Vector2 fixedVector)
        {
            return new()
            {
                asVector2D = true,
                planeType = PlaneType.XY,
                setter2D = fixedVector
            };
        }

        public static implicit operator Vector3Setter(float num)
        {
            return new()
            {
                asVector2D = false,
                setter3D = num
            };
        }

        #endregion
    }
}

