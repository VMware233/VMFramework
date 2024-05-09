using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using EnumsNET;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.MouseEvent
{
    public enum ObjectDimensions
    {
        [LabelText("2D")]
        TWO_D,

        [LabelText("3D")]
        THREE_D
    }

    [ManagerCreationProvider(ManagerType.EventCore)]
    public sealed class MouseEventManager : UniqueMonoBehaviour<MouseEventManager>
    {
        private const string DEBUGGING_CATEGORY = "Only For Debugging";
        
        [SerializeField]
        private Camera fixedBindCamera;

        [ShowInInspector]
        [HideInEditorMode]
        public static Camera bindCamera;

        public static float detectDistance2D => GameCoreSettingBase.mouseEventGeneralSetting.detectDistance2D;

        [LabelText("优先检测哪个维度的物体")]
        public ObjectDimensions dimensionsDetectPriority = ObjectDimensions.TWO_D;

        [LabelText("忽略检测的组")]
        [PropertySpace(SpaceAfter = 10)]
        public List<string> groupNamesDisabled = new();

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        public static List<MouseEventManager> allMouseEventCtrls = new();

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger currentHoverTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger lastHoverTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger leftMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger rightMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger middleMouseUpDownTrigger;

        [BoxGroup(DEBUGGING_CATEGORY), ReadOnly, ShowInInspector]
        private MouseEventTrigger dragTrigger;

        protected override void Awake()
        {
            if (!allMouseEventCtrls.Contains(this))
            {
                allMouseEventCtrls.Add(this);
            }

            if (allMouseEventCtrls.Count > 1)
            {
                Debug.LogWarning($"MouseEventController总数超出1，" + 
                                 $"这可能会占用更多性能，且有些事件可能会重复触发多次，请设置好过滤");
            }
        }

        private void Start()
        {
            if (fixedBindCamera != null)
            {
                bindCamera = fixedBindCamera;
            }
            else
            {
                bindCamera = Camera.main;
            }
        }

        private void Update()
        {

            currentHoverTrigger = DetectTrigger();

            var currentHoverTriggerIsNull = currentHoverTrigger == null;
            var lastHoverTriggerIsNull = lastHoverTrigger == null;

            #region Pointer Enter & Leave & Hover

            if (currentHoverTriggerIsNull)
            {
                // Pointer Leave
                if (lastHoverTriggerIsNull == false)
                {
                    lastHoverTrigger.Invoke(MouseEventType.PointerLeave);
                }
            }
            else
            {

                // Pointer Leave & Enter
                if (currentHoverTrigger != lastHoverTrigger)
                {
                    if (lastHoverTriggerIsNull == false)
                    {
                        lastHoverTrigger.Invoke(MouseEventType.PointerLeave);
                    }

                    currentHoverTrigger.Invoke(MouseEventType.PointerEnter);
                }

                // Pointer Hover
                currentHoverTrigger.Invoke(MouseEventType.PointerHover);
            }

            #endregion

            #region Left Mouse Button Up & Down

            if (leftMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(0))
                    {
                        leftMouseUpDownTrigger = currentHoverTrigger;

                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonDown);
                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonStay);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == leftMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(0))
                    {
                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonUp);
                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonClick);

                        leftMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(0))
                    {
                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonStay);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(0))
                    {
                        leftMouseUpDownTrigger.Invoke(MouseEventType.LeftMouseButtonUp);

                        leftMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Right Mouse Button Up & Down

            if (rightMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(1))
                    {
                        rightMouseUpDownTrigger = currentHoverTrigger;

                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonDown);
                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonStay);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == rightMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(1))
                    {
                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonUp);
                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonClick);

                        rightMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(1))
                    {
                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonStay);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(1))
                    {
                        rightMouseUpDownTrigger.Invoke(MouseEventType.RightMouseButtonUp);

                        rightMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Middle Mouse Button Up & Down

            if (middleMouseUpDownTrigger == null)
            {
                if (currentHoverTriggerIsNull == false)
                {
                    //Down
                    if (Input.GetMouseButtonDown(2))
                    {
                        middleMouseUpDownTrigger = currentHoverTrigger;

                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonDown);
                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonStay);
                    }
                }
            }
            else
            {
                if (currentHoverTrigger == middleMouseUpDownTrigger)
                {
                    //Up & Click
                    if (Input.GetMouseButtonUp(2))
                    {
                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonUp);
                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonClick);

                        middleMouseUpDownTrigger = null;
                    }
                    //Stay
                    else if (Input.GetMouseButton(2))
                    {
                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonStay);
                    }
                }
                else
                {
                    //Up
                    if (Input.GetMouseButtonUp(2))
                    {
                        middleMouseUpDownTrigger.Invoke(MouseEventType.MiddleMouseButtonUp);

                        middleMouseUpDownTrigger = null;
                    }
                }
            }

            #endregion

            #region Any Mouse Button Up & Down

            if (currentHoverTriggerIsNull == false)
            {
                //Down
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    currentHoverTrigger.Invoke(MouseEventType.AnyMouseButtonDown);
                }

                //Up
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
                {
                    currentHoverTrigger.Invoke(MouseEventType.AnyMouseButtonUp);
                }

                //Stay
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButtonUp(2))
                {
                    currentHoverTrigger.Invoke(MouseEventType.AnyMouseButtonStay);
                }
            }

            #endregion

            #region Drag Begin & Stay & End

            if (dragTrigger == null)
            {
                // Drag Begin
                if (currentHoverTriggerIsNull == false && currentHoverTrigger.draggable)
                {
                    var triggerDrag = false;

                    foreach (var mouseType in currentHoverTrigger.dragButton.GetFlags())
                    {
                        if (mouseType == MouseButtonType.LeftButton && Input.GetMouseButton(0))
                        {
                            triggerDrag = true;
                            break;
                        }

                        if (mouseType == MouseButtonType.RightButton && Input.GetMouseButton(1))
                        {
                            triggerDrag = true;
                            break;
                        }

                        if (mouseType == MouseButtonType.MiddleButton && Input.GetMouseButton(2))
                        {
                            triggerDrag = true;
                            break;
                        }
                    }

                    if (triggerDrag)
                    {
                        dragTrigger = currentHoverTrigger;

                        dragTrigger.Invoke(MouseEventType.DragBegin);
                    }
                }
            }
            else
            {
                var keepDragging = false;

                foreach (var mouseType in dragTrigger.dragButton.GetFlags())
                {
                    if (mouseType == MouseButtonType.LeftButton && Input.GetMouseButton(0))
                    {
                        keepDragging = true;
                        break;
                    }

                    if (mouseType == MouseButtonType.RightButton && Input.GetMouseButton(1))
                    {
                        keepDragging = true;
                        break;
                    }

                    if (mouseType == MouseButtonType.MiddleButton && Input.GetMouseButton(2))
                    {
                        keepDragging = true;
                        break;
                    }
                }

                if (keepDragging)
                {
                    dragTrigger.Invoke(MouseEventType.DragStay);
                }
                else
                {
                    dragTrigger.Invoke(MouseEventType.DragEnd);

                    dragTrigger = null;
                }
            }

            #endregion

            lastHoverTrigger = currentHoverTrigger;
        }


        private MouseEventTrigger DetectTrigger()
        {
            if (dimensionsDetectPriority == ObjectDimensions.TWO_D)
            {

                MouseEventTrigger detected2D = Detect2DTrigger();
                if (detected2D != null)
                {
                    return detected2D;
                }

                return Detect3DTrigger();
            }

            MouseEventTrigger detected3D = Detect3DTrigger();
            if (detected3D != null)
            {
                return detected3D;
            }

            return Detect2DTrigger();
        }

        private static MouseEventTrigger Detect3DTrigger()
        {
            var ray = bindCamera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (Physics.Raycast(ray, out var hit3D))
            {
                MouseEventTrigger detectResult = hit3D.collider.gameObject.GetComponent<MouseEventTrigger>();

                if (detectResult == null)
                {
                    return default;
                }

                if (detectResult.objectDim == ObjectDimensions.THREE_D)
                {
                    return detectResult;
                }

                return default;
            }

            return default;
        }

        private static MouseEventTrigger Detect2DTrigger()
        {
            RaycastHit2D hit2D = default;

            Ray ray = bindCamera.ScreenPointToRay(Input.mousePosition);

            float distance = -1;

            //2D射线检测
            var hitDirections = new List<Vector2>
            {
                Vector2.left,
                Vector2.right,
                Vector2.down,
                Vector2.up
            };

            foreach (Vector2 direction in hitDirections)
            {

                RaycastHit2D newHit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), direction,
                    detectDistance2D);
                if (newHit.collider)
                {
                    Vector3 colliderPos =
                        bindCamera.WorldToScreenPoint(newHit.collider.gameObject.transform.position);
                    colliderPos.z = 0;

                    float newDistance = Vector3.Distance(colliderPos, Input.mousePosition);

                    if (newDistance < distance || distance < 0)
                    {
                        distance = newDistance;
                        hit2D = newHit;
                    }
                }

            }

            if (distance >= 0)
            {
                MouseEventTrigger detectResult = hit2D.collider.gameObject.GetComponent<MouseEventTrigger>();

                if (detectResult == null)
                {
                    return null;
                }

                if (detectResult.objectDim == ObjectDimensions.TWO_D)
                {
                    return detectResult;
                }

                return null;
            }

            return null;
        }
    }
}
