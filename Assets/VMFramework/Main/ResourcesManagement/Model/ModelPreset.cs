using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;
using Object = UnityEngine.Object;

namespace VMFramework.ResourcesManagement
{
    public class ModelImportSetting : BaseConfig
    {
        [LabelText("模型预制体")]
        [GamePrefabID(typeof(ModelPreset))]
        [IsNotNullOrEmpty]
        [SerializeField, JsonProperty]
        protected string modelPrefabID;

        [LabelText("覆写位置")]
        [SerializeField, JsonProperty]
        protected bool overridePosition = true;

        [LabelText("位置")]
        [SerializeField, JsonProperty]
        [Indent]
        [ShowIf(nameof(overridePosition))]
        protected IChooserConfig<Vector3> position = new SingleValueChooserConfig<Vector3>();

        [LabelText("覆写旋转")]
        [SerializeField, JsonProperty]
        protected bool overrideRotation = false;

        [LabelText("旋转")]
        [SerializeField, JsonProperty]
        [Indent]
        [ShowIf(nameof(overrideRotation))]
        protected IChooserConfig<Vector3> rotation = new SingleValueChooserConfig<Vector3>();

        [LabelText("覆写缩放")]
        [SerializeField, JsonProperty]
        protected bool overrideScale = false;

        [LabelText("缩放")]
        [ShowIf(nameof(overrideScale))]
        [Indent]
        [SerializeField, JsonProperty]
        protected IChooserConfig<float> scale = new SingleValueChooserConfig<float>(1);

        protected GameObject modelPrefab =>
            GamePrefabManager.GetGamePrefabStrictly<ModelPreset>(modelPrefabID).GetPrefab();

        public GameObject GetModelInstance(Transform parent)
        {
            var newInstance = Object.Instantiate(modelPrefab, parent);

            if (overridePosition)
            {
                newInstance.transform.localPosition = position.GetValue();
            }

            if (overrideRotation)
            {
                newInstance.transform.localEulerAngles = rotation.GetValue();
            }

            if (overrideScale)
            {
                newInstance.transform.localScale = scale.GetValue() * Vector3.one;
            }

            return newInstance;
        }

        #region GUI

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            position ??= new SingleValueChooserConfig<Vector3>();
            rotation ??= new SingleValueChooserConfig<Vector3>();
            scale ??= new SingleValueChooserConfig<float>(1);
        }

        #endregion

        #region JSON Serialization

        public bool ShouldSerializeposition()
        {
            return overridePosition == true;
        }

        public bool ShouldSerializerotation()
        {
            return overrideRotation == true;
        }

        public bool ShouldSerializescale()
        {
            return overrideScale == true;
        }

        #endregion
    }

    public class ModelPreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "model";

        [LabelText("现成的模型预制体")]
        [Required]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public GameObject readyMadeModelPrefab;

        public GameObject GetPrefab()
        {
            return readyMadeModelPrefab;
        }
    }
}
