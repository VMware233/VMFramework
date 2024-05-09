using UnityEngine;
using UnityEngine.UI;

public static class CanvasBasis
{
    public static (Canvas canvas, CanvasScaler canvasScaler, GraphicRaycaster graphicRaycaster) CreateCanvas(
        this Transform parent, string name = "Canvas")
    {
        GameObject canvasObject = new GameObject(name);
        canvasObject.transform.SetParent(parent);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        GraphicRaycaster graphicRaycaster = canvasObject.AddComponent<GraphicRaycaster>();
        return (canvas, canvasScaler, graphicRaycaster);
    }
}
