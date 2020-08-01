using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using DG.Tweening;
using SGUI.Base;
using SGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public abstract class SGameObject
    {

        protected GameObject gameObject;

        private AnchorType anchorType;

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public SGameObject ParentSGameObject
        {
            get { return parentSGameObject; }
            set { parentSGameObject = value; }
        }

        /// <summary>
        /// rectTransform.sizeDeltaがStrechだとマイナスになったりするのでその回避用
        /// </summary>
        public Vector2 ApparentRectSize
        {
            get
            {
                var anchorMax = rectTransform.anchorMax;
                var anchorMin = rectTransform.anchorMin;
                var offsetMax = rectTransform.offsetMax;
                var offsetMin = rectTransform.offsetMin;
                float rectSizeX = 0;
                float rectSizeY = 0;
                if ((anchorMax - anchorMin).y != 0)
                {
                    if (parentSGameObject == null) rectSizeY = Screen.height;
                    else rectSizeY = parentSGameObject.ApparentRectSize.y - offsetMin.y + offsetMax.y;

                }
                else
                {
                    rectSizeY = rectTransform.sizeDelta.y;
                }
                if ((anchorMax - anchorMin).x != 0)
                {
                    if (parentSGameObject == null) rectSizeX = Screen.width;
                    else rectSizeX = parentSGameObject.ApparentRectSize.x - offsetMin.x + offsetMax.x;
                }
                else
                {
                    rectSizeX = rectTransform.sizeDelta.x;
                }
                return new Vector2(rectSizeX, rectSizeY);
            }
        }

        private SGameObject parentSGameObject;

        private Sequence sequence = DOTween.Sequence ();

        public bool IsAcive { get { return gameObject.activeSelf; } }

        public string Name
        {
            get
            {
                return name;
            }
        }
        protected string name;

        public RectTransform RectTransform
        {
            get
            {
                return rectTransform;
            }
        }

        protected RectTransform rectTransform;

        protected SGameObject (SGameObject parent, string name, Func<GameObject> initializationMethod)
        {
            this.gameObject = initializationMethod.Invoke () as GameObject;
            rectTransform = gameObject.GetComponent<RectTransform> ();
            SetParentSGameObject (parent);
            this.name = name;
        }

        public void Dispose()
        {
            GameObject.Destroy(gameObject);
        }

        public Vector2 RectSize
        {
            get
            {
                var rect = this.gameObject.GetComponent<RectTransform> ();
                return new Vector2 (rect.sizeDelta.x, rect.sizeDelta.y);
            }
            set
            {
                var rect = this.gameObject.GetComponent<RectTransform> ();
                rect.sizeDelta = new Vector2 (value.x, value.y);
            }
        }

        public void SetActive (bool isActive)
        {
            //gameObject.transform.position = initialPos;
            this.gameObject.SetActive (isActive);
        }


        public void Animate ()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup> ();

            if (sequence.IsPlaying ())
            {
                sequence.Complete ();
                sequence.Kill ();
            }
            ts.DOFade (0, 0);
            this.rectTransform.AddLocalPosY (-20f);
            sequence = DOTween.Sequence ()
                .Append (gameObject.transform.DOLocalMoveY (20f, 0.5f).SetRelative ())
                .SetEase (Ease.InCubic)
                .Join (ts.DOFade (1f, 0.5f))
                .OnComplete (() => { });
        }

        public void DisplayIn ()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup> ();
            if (sequence.IsPlaying ())
            {
                sequence.Complete ();
                sequence.Kill ();
            }
            var worldPos = gameObject.transform
                .InverseTransformPoint (gameObject.transform.position);
            var topLeftWorldPos = Utils.getScreenTopLeft ();
            var merginToLeftTop = gameObject.transform.InverseTransformPoint (topLeftWorldPos);
            var bottomRightWorldPos = Utils.getScreenBottomRight ();
            var merginToRightTop = gameObject.transform.InverseTransformPoint (bottomRightWorldPos);

            var pos = new Vector3 (rectTransform.anchoredPosition.x,
                this.rectTransform.anchoredPosition.y);

            //rectTransform.AddLocalPosY(merginToLeftTop.y + RectSize.y);
            rectTransform.AddLocalPosX (merginToLeftTop.x - RectSize.x);

            sequence = DOTween.Sequence ()
                .Append (rectTransform.DOAnchorPosX (pos.x, 0.8f))
                .SetEase (Ease.OutBounce);
        }

        public void Popup ()
        {
            if (sequence.IsPlaying ())
            {
                sequence.Complete ();
                sequence.Kill ();
            }
            var rectsize = new Vector2 (RectSize.x, RectSize.y);
            sequence = DOTween.Sequence ()
                .Append (rectTransform.DOSizeDelta (new Vector2 (rectsize.x + 20, RectSize.y + 20), 0.4f))
                .Append (rectTransform.DOSizeDelta (new Vector2 (rectsize.x - 10, RectSize.y - 20), 0.4f))
                .Append (rectTransform.DOSizeDelta (new Vector2 (rectsize.x, RectSize.y), 0.4f));
            sequence.SetEase (Ease.OutBack);
        }

        public void Punch ()
        {
            if (sequence.IsPlaying ())
            {
                sequence.Complete ();
                sequence.Kill ();
            }

            var pivot = rectTransform.pivot;
            var pivotOffsets = new Vector2 (0.5f, 0.5f) - pivot;
            SetPivot (0.5f, 0.5f);
            //var pospre = new Vector2(gameObject.transform.position.x,
            //    gameObject.transform.position.y);
            //var pospre2 = new Vector2(rectTransform.anchoredPosition.x,
            //    rectTransform.anchoredPosition.y);

            //var pivotOffsets = new Vector2(0.5f, 0.5f) - pivot; 

            //SetPivot(new Vector2(0.0f, 0.1f));

            //gameObject.transform.AddLocalPosX(-RectSize.x / 2);
            //gameObject.transform.AddLocalPosY(RectSize.y / 2);

            //        var pos2 = gameObject.transform.position;
            //        var posaf2 = new Vector2(rectTransform.anchoredPosition.x,
            //rectTransform.anchoredPosition.y);

            sequence = DOTween.Sequence ()
                .Append (gameObject.transform.DOPunchScale (new Vector3 (0.2f, 0.2f), 1f, 1))
                .SetEase (Ease.OutBack)
                .OnComplete (() =>
                {
                    SetPivot (0.0f, 1f);
                });
        }

        private void SetPivot (float x, float y)
        {
            var newPivot = new Vector2 (x, y);
            var pivotOffset = newPivot - rectTransform.pivot;
            rectTransform.pivot = newPivot;
            rectTransform.AddLocalPosX (RectSize.x * pivotOffset.x);
            rectTransform.AddLocalPosY (RectSize.y * pivotOffset.y);
        }

        public virtual SGameObject SetBackGroundColor (ColorType colorType, float alpha = 1)
        {
            var color = gameObject.GetComponent<Image> ();
            if (!color) color = gameObject.AddComponent<Image> ();
            if (color) color.color = Utils.GetColor (colorType, alpha);
            return this;
        }

        public virtual SGameObject SetBackGroundColor (Color _color)
        {
            var color = gameObject.GetComponent<Image> ();
            if (!color) color = gameObject.AddComponent<Image> ();
            if (color) color.color = _color;
            return this;
        }

        public virtual SGameObject SetParentSGameObject (SGameObject parent)
        {
            if (parent?.gameObject == null)
            {
                Debug.Log(parent);
            }
            if (parent != null && parent.gameObject.transform != null)
            {
                if (parent is ILayoutObject)
                {
                    ILayoutObject layoutObject = parent as ILayoutObject;
                    layoutObject.AddItem (this);
                }
                else
                {
                    gameObject.transform.SetParent (parent.GameObject.transform, false);
                    this.parentSGameObject = parent;
                }
            }
            return this;
        }

        public virtual SGameObject SetRectSizeByRatio (float ratioX, float ratioY)
        {
            if (parentSGameObject == null) return this;
            SetRectSize(parentSGameObject.RectSize.x * ratioX,
                parentSGameObject.RectSize.y * ratioY);
            return this;
        }

        public virtual SGameObject SetRectSize (float width, float height)
        {
            if (anchorType == AnchorType.FullStretch || anchorType == AnchorType.HorizontalStretch
                || anchorType == AnchorType.VerticalStretch)
            {
                var anchor = anchorType;
                RectSize = new Vector2(width, height);
                SetAnchorType(anchor);
            }
            else
            {
                RectSize = new Vector2(width, height);
            }
            RectSize = new Vector2(width, height);
            return this;
        }

        public virtual SGameObject SetLocalPosByRatio (float posXratio, float posYratio)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPosX (posXratio * parentSGameObject.RectSize.x);
            gameObject.transform.SetLocalPosY (-(posYratio * parentSGameObject.RectSize.y));
            return this;
        }

        public virtual SGameObject SetLocalPos (float posX, float posY)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPos (posX, -posY);
            return this;
        }

        public virtual SGameObject SetPosAndSize(RectInfo rectInfo)
        {
            SetLocalPos(rectInfo.PosX, rectInfo.PosY);
            SetRectSize(rectInfo.Width, rectInfo.Height);
            return this;
        }

        public virtual SGameObject SetPosAndSize(float posX, float posY, float width, float height)
        {
            SetLocalPos(posX, posY);
            SetRectSize(width, height);
            return this;
        }

        public virtual SGameObject SetPosAndSizeByRatio(float posXratio, float posYratio, float widthRatio, float heightRatio)
        {
            SetLocalPosByRatio(posXratio, posYratio);
            SetRectSizeByRatio(widthRatio, heightRatio);
            return this;
        }

        public virtual SGameObject SetBackGroundImage (string sourceName)
        {
            var imageSource = Resources.Load<Sprite> (sourceName);
            var image = gameObject.GetComponent<Image> ();
            if (!image) image = gameObject.AddComponent<Image> ();
            image.sprite = imageSource;
            return this;
        }

        // public void ClearComponents () { 
        //     GameObject.Destroy(gameObject);
        //     gameObject = UIFactory.CreateBaseRect(parentSGameObject.GameObject, name);
        //     var rect = gameObject.GetComponent<RectTransform>().sizeDelta;
        //     gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSize.x, RectSize.y);
        // }

        public void ClearComponents ()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy (child.gameObject);
            }
            var gridLayout = gameObject.GetComponent<GridLayoutGroup> ();
            var verticalLayout = gameObject.GetComponent<VerticalLayoutGroup> ();
            //GameObject.DestroyImmediate(layout0, true); 
            Destroy (gridLayout, verticalLayout);
        }
        IEnumerator Destroy (params Component[] components)
        {
            yield return new WaitForEndOfFrame ();
            components.ToList ().ForEach (c => Destroy (c));
        }

        // IEnumerator Destroy (Component component)
        // {
        //     yield return new WaitForEndOfFrame ();
        //     Destroy (component);
        // }

        public Action GetFunction (Action function)
        {
            return new Action (() => function ());
        }

        public Action<string> GetFunction (Action<string> function)
        {
            return new Action<string> (function);
        }

        #region RectSetter

        public SGameObject SetOffset (float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2 (maxX, maxY);
            rectTransform.offsetMin = new Vector2 (minX, minY);
            return this;
        }

        public SGameObject SetAnchorType(AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.TopLeft:
                    {
                        rectTransform.SetTopLeftAnchor();
                        this.anchorType = AnchorType.TopLeft;
                        return this;
                    }
                case AnchorType.MiddleCenter:
                    {
                        rectTransform.SetMiddleCenterAnchor();
                        this.anchorType = AnchorType.MiddleCenter;
                        return this;
                    }
                case AnchorType.HorizontalStretch:
                    {
                        rectTransform.SetHorizontalStretchAnchor();
                        this.anchorType = AnchorType.HorizontalStretch;
                        return this;
                    }
                case AnchorType.VerticalStretch:
                    {
                        rectTransform.SetMiddleCenterAnchor();
                        this.anchorType = AnchorType.VerticalStretch;
                        return this;
                    }
                case AnchorType.FullStretch:
                    {
                        rectTransform.SetFullStretchAnchor();
                        this.anchorType = AnchorType.FullStretch;
                        return this;
                    }
            }
            return this;
        }

        //public enum AnchorType
        //{
        //    TopLeft, TopCenter, TopRight,
        //    MiddleLeft, MiddleCenter, MiddleRight,
        //    BottomLeft, BottomCenter, BottomRight,
        //    HorizontalStretch, VerticalStretch,
        //    FullStretch
        //}

        public SGameObject SetTopLeftAnchor()
        {
            //rectTransform.SetTopLeftAnchor();
            SetAnchorType(AnchorType.TopLeft);
            return this;
        }

        public SGameObject SetMiddleCenterAnchor()
        {
            //rectTransform.SetMiddleCenterAnchor();
            SetAnchorType(AnchorType.MiddleCenter);
            return this;
        }

        public SGameObject SetHorizontalStretchAnchor()
        {
            //rectTransform.SetHorizontalStretchAnchor();
            SetAnchorType(AnchorType.HorizontalStretch);
            return this;
        }

        public SGameObject SetVerticalStretchAnchor()
        {
           // rectTransform.SetVerticalStretchAnchor();
            SetAnchorType(AnchorType.VerticalStretch);
            return this;
        }

        public SGameObject SetMiddleLeftAnchor()
        {
            rectTransform.SetMiddleLeftAnchor();
            return this;
        }

        public SGameObject SetFullStretchAnchor()
        {
            //rectTransform.SetFullStretchAnchor();
            SetAnchorType(AnchorType.FullStretch);
            return this;
        }


        #endregion

    }
}