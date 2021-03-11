using Assets.Scripts.Extensions;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGCanvas : EGGameObject
    {
        /// <summary>
        /// Canvasコンポーネント
        /// </summary>
        public Canvas CanvasComponent;

        /// <summary>
        /// Canvasオブジェクトのラッパークラス
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isStatic"></param>
        public EGCanvas(string name = "EGCanvas", bool isStatic = false) : base(name: name)
        {
            Utils.TryCreateEventSystem();
            CanvasComponent = gameObject.GetOrAddComponent<Canvas>();
            CanvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
            if (Camera.main == null)
            {
                Debug.Log("Failed to generate Canvas object because Main Camera Object is not found.");
                gameObject.DestroySelf();
                return;
            }

            CanvasComponent.worldCamera = Camera.main;
            gameObject.AddComponent<GraphicRaycaster>();
            if (isStatic)
            {
                CanvasComponent.sortingOrder = 1000;
            }
            else
            {
                CanvasStack.Push(this);
            }

            SetRelativePosition(0, 0).SetRelativeSize(1, 1);
        }

        /// <summary>
        /// GameObjectをDestroyする
        /// </summary>
        public void DestroySelf()
        {
            CanvasStack.RemoveByObjectName(gameObject.name);
            gameObject.DestroySelf();
        }
    }
}