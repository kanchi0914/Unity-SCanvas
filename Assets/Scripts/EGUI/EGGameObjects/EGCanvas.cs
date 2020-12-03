using System.Linq;
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
            ;
            CanvasComponent = gameObject.GetOrAddComponent<Canvas>();
            CanvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
            var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (mainCamera == null)
            {
                Debug.Log("Failed to generate Canvas object because Main Camera Object is not found.");
                gameObject.DestroySelf();
                return;
            }

            CanvasComponent.worldCamera = mainCamera;
            
            // var scaler = gameObject.AddComponent<CanvasScaler>();
            // scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            // scaler.referenceResolution = new Vector2(800, 600);
            
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
            GameObject.DestroyImmediate(gameObject);
        }
    }
}