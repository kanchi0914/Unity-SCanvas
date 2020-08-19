using Assets.Scripts.Extensions;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGCanvas : EGGameObject
    {
        public Canvas CanvasComponent;
        public EGCanvas(GameObject parent, string name, bool isStatic) : base(null, name)
        {
            CanvasComponent = gameObject.TryAddComponent<Canvas>();
            if (parent != null)
            {
                CanvasComponent.overrideSorting = true;
            }
            else
            {
                CanvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
                var mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
                CanvasComponent.worldCamera = mainCamera;
                gameObject.AddComponent<CanvasScaler> ();
            }
            gameObject.AddComponent<GraphicRaycaster> ();
            if (isStatic)
            {
                CanvasComponent.sortingOrder = 1000;
            }
            else
            {
                CanvasStack.Push(this);
            }
            gameObject.SetLocalPosByRatio(0, 0).SetRectSizeByRatio(1, 1);
        }
    }
}