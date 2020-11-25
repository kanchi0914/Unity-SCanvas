using System;
using System.Collections;
using Assets.Scripts.Extensions;
using UnityEngine;
using EGUI.Base;
using EGUI.EGGameObjects.Base;

namespace EGUI.GameObjects
{
    public class EGGameObject
    {
        /// <summary>
        /// インスタンス生成時に生成したGameObject
        /// </summary>
        public GameObject gameObject { get; private set; }
        
        /// <summary>
        /// RectTransformコンポーネント
        /// </summary>
        public RectTransform rectTransform
        {
            get => gameObject.GetRectTransform();
        }

        /// <summary>
        /// RectTransform.sizeDelta
        /// </summary>
        public Vector2 RectSize
        {
            get => rectTransform.sizeDelta;
            set => rectTransform.sizeDelta = value;
        }

        /// <summary>
        /// GameObjectのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクトをラップするEGGameObject</param>
        /// <param name="name">オブジェクト名</param>
        public EGGameObject(EGGameObject parent) : this(parent.gameObject){}

        /// <summary>
        /// GameObjectのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">オブジェクト名</param>
        public EGGameObject
        (
            GameObject parent = null,
            string name = "GameObject"
        )
        {
            gameObject = new GameObject(name);
            if (parent != null) gameObject.transform.SetParent(parent.transform, false);
            else if (GlobalSettings.createsNewCanvasWhenParentIsNull && !(this is EGCanvas))
            {
                var canvas = new EGCanvas("Canvas");
                gameObject.transform.SetParent(canvas.gameObject.transform, false);
            }
            gameObject.GetOrAddComponent<RectTransform>().SetTopLeftAnchor();
            SetLocalPos(0, 0).SetRectSize(100, 100);
        }
        
        /// <summary>
        /// GameObjectをDestroyする
        /// </summary>
        public void DestroySelf()
        {  
            GameObject.DestroyImmediate(gameObject);
        }

        public EGGameObject SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            return this;
        }
        
        public EGGameObject SetRectSizeByRatio(float ratioX, float ratioY)
        {
            rectTransform.SetRectSize(rectTransform.GetParentRectSize().x * ratioX,
                rectTransform.GetParentRectSize().y * ratioY);
            return this;
        }

        public EGGameObject SetRectSize(float width, float height)
        {
            rectTransform.SetRectSize(width, height);
            return this;
        }

        public EGGameObject SetLocalPosByRatio(float posXratio, float posYratio)
        {
            rectTransform.SetLocalPosByRatio(posXratio, posYratio);
            return this;
        }

        public EGGameObject SetLocalPos(float posX, float posY)
        {
            rectTransform.SetAnchoredPos(posX, -posY);
            return this;
        }
        
        public EGGameObject SetPivot(float x, float y)
        {
            rectTransform.SetPivot(x, y);
            return this;
        }

        public EGGameObject SetPresetRect(RectInfo rectInfo)
        {
            rectTransform.SetPresetRect(rectInfo);
            return this;
        }
        
        public EGGameObject SetTopLeftAnchor()
        {
            rectTransform.SetTopLeftAnchor();
            return this;
        }
        
        public EGGameObject SetTopCenterAnchor()
        {
            rectTransform.SetTopCenterAnchor();
            return this;
        }
        
        public EGGameObject SetTopRightAnchor()
        {
            rectTransform.SetTopRightAnchor();
            return this;
        }
        
        public EGGameObject SetMiddleLeftAnchor()
        {
            rectTransform.SetMiddleLeftAnchor();
            return this;
        }
        
        public EGGameObject SetMiddleCenterAnchor()
        {
            rectTransform.SetMiddleCenterAnchor();
            return this;
        }
        
        public EGGameObject SetMiddleRightAnchor()
        {
            rectTransform.SetMiddleRightAnchor();
            return this;
        }
        
        public EGGameObject SetBottomLeftAnchor()
        {
            rectTransform.SetBottomLeftAnchor();
            return this;
        }
        
        public EGGameObject SetBottomCenterAnchor()
        {
            rectTransform.SetBottomCenterAnchor();
            return this;
        }
        
        public EGGameObject SetBottomRightAnchor()
        {
            rectTransform.SetBottomRightAnchor();
            return this;
        }

        
        public EGGameObject SetHorizontalStretchAnchor()
        {
            rectTransform.SetHorizontalStretchAnchor();
            return this;
        }
        
        public EGGameObject SetHorizontalStretchWithTopPivotAnchor()
        {
            rectTransform.SetHorizontalStretchWithTopPivotAnchor();
            return this;
        }
        
        public EGGameObject SetHorizontalStretchWithBottomPivotAnchor()
        {
            rectTransform.SetHorizontalStretchWithBottomPivotAnchor();
            return this;
        }
        
        public EGGameObject SetVerticalStretchAnchor()
        {
            rectTransform.SetVerticalStretchAnchor();
            return this;
        }
        
        public EGGameObject SetVerticalStretchWithLeftPivotAnchor()
        {
            rectTransform.SetVerticalStretchWithLeftPivotAnchor();
            return this;
        }
        
        public EGGameObject SetVerticalStretchWithRightPivotAnchor()
        {
            rectTransform.SetVerticalStretchWithRightPivotAnchor();
            return this;
        }

        public EGGameObject SetFullStretchAnchor()
        {
            rectTransform.SetFullStretchAnchor();
            return this;
        }
        
              
        public EGGameObject SetOnClick(Action action)
        {
            gameObject.SetOnClick(action);
            return this;
        }

        public EGGameObject AddOnClick(Action action)
        {
            gameObject.AddOnClick(action);
            return this;
        }

        public EGGameObject SetImage(string imageFilePath)
        {
            gameObject.SetImage(imageFilePath);
            return this;
        }
        
        public EGGameObject SetImageSprite(Sprite sprite)
        {
            var aaa = gameObject;
            gameObject.SetImageSprite(sprite);
            return this;
        }
        
        public EGGameObject SetImageColor(Color color, float? alpha = null)
        {
            gameObject.SetImageColor(color, alpha);
            return this;
        }
        
    }
}