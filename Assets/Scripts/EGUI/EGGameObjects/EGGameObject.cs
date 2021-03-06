﻿using System;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.EGUI;
using Assets.Scripts.EGUI.MonoBehaviourScripts;
using Assets.Scripts.Extensions;
using UnityEngine;
using EGUI.Base;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGGameObject
    {
        public bool IsAutoResizing { get; set; } = false;

        public float WidthRatio { get; set; }
        public float HeightRatio { get; set; }

        /// <summary>
        /// The GameObject generated in the constructor.
        /// </summary>
        public GameObject gameObject { get; private set; }

        /// <summary>
        /// RectTransform Component
        /// </summary>
        public RectTransform rectTransform
        {
            get => gameObject.GetRectTransform();
        }

        public Image ImageComponent
        {
            get => gameObject.GetComponent<Image>();
        }

        private static int a = 0;

        /// <summary>
        /// Duplicate this object and return it.
        /// </summary>
        /// <returns></returns>
        public EGGameObject Duplicate()
        {
            var gameObjectClone = Utils.Clone(gameObject);
            var duplicated = new EGGameObject(this, name: gameObject.name, self: gameObjectClone);
            duplicated.WidthRatio = WidthRatio;
            duplicated.HeightRatio = HeightRatio;
            return duplicated;
        }

        /// <summary>
        /// The value of rectTransform.sizeDelta.
        /// </summary>
        public Vector2 RectSize
        {
            get => rectTransform.sizeDelta;
            set => rectTransform.sizeDelta = value;
        }

        /// <summary>
        /// Initializes a new instance of the EGGameObject class and wraps given GameObject.
        /// </summary>
        /// <param name="gameObject"></param>
        public EGGameObject SetGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the EGGameObject class, generating a new GameObject.
        /// The parent of the generated Object is set to the given GameObject. 
        /// </summary>
        /// <param name="parent">The GameObject set to tha parent.</param>
        /// <param name="name">The name of the generated GameObject.</param>
        public EGGameObject
        (
            GameObject parent,
            string name = "GameObject",
            GameObject self = null,
            bool destroysGameObjectIfExists = false
        )
        {
            if (self != null)
            {
                gameObject = self;
                gameObject.GetOrAddComponent<EGUIObjectInfo>().Init(this);
                return;
            }

            gameObject = new GameObject(name);
            gameObject.GetOrAddComponent<EGUIObjectInfo>().Init(this);

            if (parent != null)
            {
                gameObject.transform.SetParent(parent.transform, false);
            }
            else if (!(this is EGCanvas))
            {
                if (CanvasStack.Stack.Count < 1)
                {
                    var canvas = new EGCanvas("Canvas");
                    gameObject.transform.SetParent(canvas.gameObject.transform, false);
                }
                else
                {
                    var canvas = CanvasStack.Stack.Peek().egCanvas;
                    gameObject.transform.SetParent(canvas.gameObject.transform, false);
                }
            }
            gameObject.GetOrAddComponent<RectTransform>().SetAnchorType(AnchorType.MiddleCenter);
            SetPosition(0, 0).SetSize(100, 100);
        }

        /// <summary>
        /// Initializes a new instance of the EGGameObject class, generating a new GameObject.
        /// The parent of the generated GameObject is set to the GameObject that wrapped by the given EGGameObject. 
        /// </summary>
        /// <param name="parent">The EGGameObject that wraps GameObject set to tha parent.</param>
        /// <param name="name">The name of the generated GameObject.</param>
        public EGGameObject(EGGameObject parent = null, string name = "GameObject", GameObject self = null, bool destroysGameObjectIfExists = false)
            : this(parent?.gameObject, name, self, destroysGameObjectIfExists)
        {
        }

        /// <summary>
        /// Destroy the wrapped GameObject.
        /// </summary>
        public void DestroySelf()
        {
            GameObject.DestroyImmediate(gameObject);
        }

        /// <summary>
        /// Calls SetActive method of the wrapped GameObject.
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public EGGameObject SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            return this;
        }

        /// <summary>
        /// Calls SetParent method of wrapped GameObject.
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public EGGameObject SetParent(EGGameObject egGameObject)
        {
            gameObject.transform.SetParent(egGameObject.gameObject.transform, false);
            return this;
        }

        /// <summary>
        /// Set the size of the rectTransform, using the ratio of height and width to its parent.
        /// </summary>
        /// <param name="widthRatio">The ratio of the height to the parent.</param>
        /// <param name="heightRatio">The ratio of the height to the parent.</param>
        /// <param name="isAutoResizing"></param>
        /// <returns></returns>
        public EGGameObject SetRelativeSize(float widthRatio, float heightRatio, bool isAutoResizing = true)
        {
            IsAutoResizing = isAutoResizing;
            WidthRatio = widthRatio;
            HeightRatio = heightRatio;
            if (isAutoResizing)
            {
                var setter = gameObject.GetOrAddComponent<RelativeSizeSetter>();
                setter.RatioX = widthRatio;
                setter.RatioY = heightRatio;
            }
            else
            {
                var setter = gameObject.GetComponent<RelativeSizeSetter>();
                GameObject.Destroy(setter);
            }

            rectTransform.SetSize(rectTransform.GetParentRectSize().x * widthRatio,
                rectTransform.GetParentRectSize().y * heightRatio);
            return this;
        }

        public T As<T>() where T : EGGameObject
        {
            return (T) this;
        }

        /// <summary>
        /// Set the size of the rectTransform. 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public EGGameObject SetSize(float width, float height)
        {
            rectTransform.SetSize(width, height);
            GameObject.Destroy(gameObject.GetComponent<RelativeSizeSetter>());
            return this;
        }

        /// <summary>
        /// Set the position of the rectTransform, using the ratio of height and width to its parent.
        /// The position is set at the value of an anchoredPosition property.
        /// </summary>
        /// <param name="posXRatio">The ratio of the position X to the parent.</param>
        /// <param name="posYRatio">The ratio of the height to the parent.</param>
        /// <returns></returns>
        public EGGameObject SetRelativePosition(float posXRatio, float posYRatio)
        {
            rectTransform.SetRelativeAnchoredPos(posXRatio, posYRatio);
            return this;
        }

        /// <summary>
        /// Set the position of the rectTransform.
        /// The position is set at the value of an anchoredPosition property.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y.</param>
        /// <returns></returns>
        public EGGameObject SetPosition(float posX, float posY)
        {
            rectTransform.SetAnchoredPos(posX, posY);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public EGGameObject SetPivot(float x, float y)
        {
            rectTransform.SetPivot(x, y);
            return this;
        }

        // /// <summary>
        // /// Set the parameters of rectTransform using the values of RectTransformInfo.
        // /// </summary>
        // /// <param name="rectInfo"></param>
        // /// <returns></returns>
        // public EGGameObject SetPresetRect(RectInfo rectInfo)
        // {
        //     rectTransform.SetPresetRect(rectInfo);
        //     return this;
        // }
        
        public EGGameObject AddEvent(EventTriggerType type, Action action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(e => action.Invoke());
            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            trigger.triggers.Add(entry);
            return this;
        }

        public EGGameObject RemoveAllEvent()
        {
            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            trigger.triggers.Clear();
            return this;
        }
        
        // public EGGameObject SetGlobalPos(Vector3 pos)
        // {
        //     var parentMono = rectTransform.parent.gameObject.GetOrAddComponent<GlobalPosSetter>();
        //     if (parentMono != null)
        //     {
        //         gameObject.SetActive(false);
        //         parentMono.StartCoroutine(SetGlobalPosCoroutine(pos));
        //     }
        //     else
        //     {
        //         parentMono.StartCoroutine(SetGlobalPosCoroutine(pos));
        //     }
        //     return this;
        // }

        private IEnumerator SetGlobalPosCoroutine(Vector3 pos)
        {
            yield return null;
            gameObject.transform.position = pos;
            gameObject.SetActive(true);
        }

        public EGGameObject SetAnchorType(AnchorType anchorType, bool keepsPosition = false)
        {
            rectTransform.SetAnchorType(anchorType, keepsPosition);
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

        public EGGameObject SetImage(Sprite sprite)
        {
            gameObject.SetImage(sprite);
            return this;
        }

        public EGGameObject SetImageColor(Color color, float? alpha = null)
        {
            gameObject.SetImageColor(color, alpha);
            return this;
        }
    }
}