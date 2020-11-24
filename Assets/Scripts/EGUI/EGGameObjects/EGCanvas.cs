﻿using Assets.Scripts.Extensions;
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
        public EGCanvas(string name, bool isStatic = false) : base(null, name)
        {
            Utils.TryCreateEventSystem();;
            CanvasComponent = gameObject.GetOrAddComponent<Canvas>();
            CanvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
            var mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
            if (mainCamera == null)
            {
                Debug.Log("Main Camera Object is not found.");
                gameObject.DestroySelf();
            }
            CanvasComponent.worldCamera = mainCamera;
            gameObject.AddComponent<CanvasScaler> ();
            gameObject.AddComponent<GraphicRaycaster> ();
            if (isStatic)
            {
                CanvasComponent.sortingOrder = 1000;
            }
            else
            {
                CanvasStack.Push(this);
            }
            SetLocalPosByRatio(0, 0).SetRectSizeByRatio(1, 1);
        }
    }
}