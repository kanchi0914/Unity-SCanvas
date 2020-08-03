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
using UnityEngine;
using UnityEngine;
using System.Threading.Tasks;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public abstract class EGGameObject
    {
        protected GameObject gameObject;

        public EGMono Mono;

        private Utils.AnchorType anchorType;

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public EGGameObject ParentEgGameObject
        {
            get { return _parentEgGameObject; }
            set { _parentEgGameObject = value; }
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
                    if (_parentEgGameObject == null) rectSizeY = Screen.height;
                    else rectSizeY = _parentEgGameObject.ApparentRectSize.y - offsetMin.y + offsetMax.y;
                }
                else
                {
                    rectSizeY = rectTransform.sizeDelta.y;
                }

                if ((anchorMax - anchorMin).x != 0)
                {
                    if (_parentEgGameObject == null) rectSizeX = Screen.width;
                    else rectSizeX = _parentEgGameObject.ApparentRectSize.x - offsetMin.x + offsetMax.x;
                }
                else
                {
                    rectSizeX = rectTransform.sizeDelta.x;
                }

                return new Vector2(rectSizeX, rectSizeY);
            }
        }

        private EGGameObject _parentEgGameObject;

        private Sequence sequence = DOTween.Sequence();

        private Vector3 actualPos;

        public bool IsAcive
        {
            get { return gameObject.activeSelf; }
        }

        public string Name
        {
            get { return name; }
        }

        protected string name;

        public RectTransform RectTransform
        {
            get { return rectTransform; }
        }


        protected RectTransform rectTransform;

        protected EGGameObject
        (
            EGGameObject parent,
            float posRatioX,
            float posRatioY,
            float widthRatio,
            float heightRatio,
            string name,
            Func<GameObject> initializationMethod
            )
        {
            gameObject = initializationMethod.Invoke();
            rectTransform = gameObject.GetComponent<RectTransform>();
            SetParentSGameObject(parent);
            SetTopLeftAnchor();
            Mono = gameObject.TryAddComponent<EGMono>();
            SetRectSizeByRatio(widthRatio, heightRatio);
            SetLocalPosByRatio(posRatioX, posRatioY);
            this.name = name;
        }

        /// <summary>
        /// GameObjectをDestroyする
        /// </summary>
        public void Dispose()
        {
            GameObject.Destroy(gameObject);
        }

        public Vector2 RectSize
        {
            get
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                return new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
            }
            set
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(value.x, value.y);
            }
        }

        public EGGameObject SetGlobalPos(Vector3 pos)
        {
            var parentMono = _parentEgGameObject.Mono;
            if (parentMono != null)
            {
                gameObject.SetActive(false);
                parentMono.StartCoroutine(SetGlobalPosCoroutine(pos));
            }
            else
            {
                Mono.StartCoroutine(SetGlobalPosCoroutine(pos));
            }

            return this;
        }

        private IEnumerator SetGlobalPosCoroutine(Vector3 pos)
        {
            yield return null;
            gameObject.transform.position = pos;
            gameObject.SetActive(true);
        }

        public void SetActive(bool isActive)
        {
            //gameObject.transform.position = initialPos;
            this.gameObject.SetActive(isActive);
        }


        public void Animate()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup>();

            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            ts.DOFade(0, 0);
            this.rectTransform.AddLocalPosY(-20f);
            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOLocalMoveY(20f, 0.5f).SetRelative())
                .SetEase(Ease.InCubic)
                .Join(ts.DOFade(1f, 0.5f))
                .OnComplete(() => { });
        }

        public void DisplayIn()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup>();
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            var worldPos = gameObject.transform
                .InverseTransformPoint(gameObject.transform.position);
            var topLeftWorldPos = Utils.getScreenTopLeft();
            var merginToLeftTop = gameObject.transform.InverseTransformPoint(topLeftWorldPos);
            var bottomRightWorldPos = Utils.getScreenBottomRight();
            var merginToRightTop = gameObject.transform.InverseTransformPoint(bottomRightWorldPos);

            var pos = new Vector3(rectTransform.anchoredPosition.x,
                this.rectTransform.anchoredPosition.y);

            //rectTransform.AddLocalPosY(merginToLeftTop.y + RectSize.y);
            rectTransform.AddLocalPosX(merginToLeftTop.x - RectSize.x);

            sequence = DOTween.Sequence()
                .Append(rectTransform.DOAnchorPosX(pos.x, 0.8f))
                .SetEase(Ease.OutBounce);
        }

        public void Popup()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            var rectsize = new Vector2(RectSize.x, RectSize.y);
            sequence = DOTween.Sequence()
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x + 20, RectSize.y + 20), 0.4f))
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x - 10, RectSize.y - 20), 0.4f))
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x, RectSize.y), 0.4f));
            sequence.SetEase(Ease.OutBack);
        }

        public void Punch()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            var pivot = rectTransform.pivot;
            var pivotOffsets = new Vector2(0.5f, 0.5f) - pivot;
            SetPivot(0.5f, 0.5f);
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

            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 1f, 1))
                .SetEase(Ease.OutBack)
                .OnComplete(() => { SetPivot(0.0f, 1f); });
        }


        public void AnimateShake()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOShakePosition(0.5f, strength: 20, vibrato: 30));
        }

        public void AnimateBlink()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            CanvasGroup imageCanvas = gameObject.TryAddComponent<CanvasGroup>();
            sequence = DOTween.Sequence()
                .Append(imageCanvas.DOFade(0.0f, 0.1f).SetLoops(5))
                .Append(imageCanvas.DOFade(1.0f, 0.0f));
        }

        private IEnumerator SetAction(Action action)
        {
            yield return null;
            action.Invoke();
        }

        //private IEnumerator SetAnime()
        //{
        //    yield return null;
        //    if (sequence.IsPlaying())
        //    {
        //        sequence.Complete();
        //        sequence.Kill();
        //    }
        //    sequence = DOTween.Sequence().
        //        Append(gameObject.transform.DOShakePosition(0.5f, strength: 30, vibrato: 50));
        //}


        private void SetPivot(float x, float y)
        {
            var newPivot = new Vector2(x, y);
            var pivotOffset = newPivot - rectTransform.pivot;
            rectTransform.pivot = newPivot;
            rectTransform.AddLocalPosX(RectSize.x * pivotOffset.x);
            rectTransform.AddLocalPosY(RectSize.y * pivotOffset.y);
        }

        public virtual EGGameObject SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            if (color) color.color = Utils.GetColor(colorType, alpha);
            return this;
        }

        public virtual EGGameObject SetBackGroundColor(Color _color)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            if (color) color.color = _color;
            return this;
        }

        public virtual EGGameObject SetParentSGameObject(EGGameObject parent)
        {
            if (parent?.gameObject == null)
            {
            }

            if (parent != null && parent.gameObject.transform != null)
            {
                if (parent is ILayoutObject)
                {
                    ILayoutObject layoutObject = parent as ILayoutObject;
                    layoutObject.AddItem(this);
                }
                else
                {
                    gameObject.transform.SetParent(parent.GameObject.transform, false);
                    this._parentEgGameObject = parent;
                }
            }

            return this;
        }

        public virtual EGGameObject SetRectSizeByRatio(float ratioX, float ratioY)
        {
            if (_parentEgGameObject == null) return this;
            SetRectSize(_parentEgGameObject.RectSize.x * ratioX,
                _parentEgGameObject.RectSize.y * ratioY);
            return this;
        }

        public virtual EGGameObject SetRectSize(float width, float height)
        {
            if (anchorType == Utils.AnchorType.FullStretch || anchorType == Utils.AnchorType.HorizontalStretch
                                                           || anchorType == Utils.AnchorType.VerticalStretch)
            {
                var anchor = anchorType;
                RectSize = new Vector2(width, height);
                SetAnchorType(anchor);
            }
            else
            {
                RectSize = new Vector2(width, height);
            }

            return this;
        }

        public virtual EGGameObject SetPresetRect(RectInfo rectInfo)
        {
            SetPosAndSize(rectInfo.PosX, rectInfo.PosY, rectInfo.Width, rectInfo.Height);
            return this;
        }

        public virtual EGGameObject SetLocalPosByRatio(float posXratio, float posYratio)
        {
            if (_parentEgGameObject == null) return this;
            gameObject.transform.SetLocalPosX(posXratio * _parentEgGameObject.RectSize.x);
            gameObject.transform.SetLocalPosY(-(posYratio * _parentEgGameObject.RectSize.y));
            return this;
        }

        public virtual EGGameObject SetLocalPos(float posX, float posY)
        {
            if (_parentEgGameObject == null) return this;
            gameObject.transform.SetLocalPos(posX, -posY);
            return this;
        }

        public virtual EGGameObject SetPosAndSize(RectInfo rectInfo)
        {
            SetLocalPos(rectInfo.PosX, rectInfo.PosY);
            SetRectSize(rectInfo.Width, rectInfo.Height);
            return this;
        }

        public virtual EGGameObject SetPosAndSize(float posX, float posY, float width, float height)
        {
            SetLocalPos(posX, posY);
            SetRectSize(width, height);
            return this;
        }

        public virtual EGGameObject SetPosAndSizeByRatio(float posXratio, float posYratio, float widthRatio,
            float heightRatio)
        {
            SetLocalPosByRatio(posXratio, posYratio);
            SetRectSizeByRatio(widthRatio, heightRatio);
            return this;
        }

        public virtual EGGameObject SetBackGroundImage(string sourceName)
        {
            var imageSource = Resources.Load<Sprite>(sourceName);
            var image = gameObject.GetComponent<Image>();
            if (!image) image = gameObject.AddComponent<Image>();
            image.sprite = imageSource;
            return this;
        }

        // public void ClearComponents () { 
        //     GameObject.Destroy(gameObject);
        //     gameObject = UIFactory.CreateBaseRect(parentSGameObject.GameObject, name);
        //     var rect = gameObject.GetComponent<RectTransform>().sizeDelta;
        //     gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSize.x, RectSize.y);
        // }

        public void ClearComponents()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            var gridLayout = gameObject.GetComponent<GridLayoutGroup>();
            var verticalLayout = gameObject.GetComponent<VerticalLayoutGroup>();
            //GameObject.DestroyImmediate(layout0, true); 
            Destroy(gridLayout, verticalLayout);
        }

        IEnumerator Destroy(params Component[] components)
        {
            yield return new WaitForEndOfFrame();
            components.ToList().ForEach(c => Destroy(c));
        }

        // IEnumerator Destroy (Component component)
        // {
        //     yield return new WaitForEndOfFrame ();
        //     Destroy (component);
        // }

        //public Action GetFunction (Action function)
        //{
        //    return new Action (() => function ());
        //}

        //public Action<string> GetFunction (Action<string> function)
        //{
        //    return new Action<string> (function);
        //}

        #region RectSetter

        public EGGameObject SetOffset(float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return this;
        }

        public EGGameObject SetAnchorType(Utils.AnchorType anchorType)
        {
            switch (anchorType)
            {
                case Utils.AnchorType.TopLeft:
                {
                    rectTransform.SetTopLeftAnchor();
                    this.anchorType = Utils.AnchorType.TopLeft;
                    return this;
                }
                case Utils.AnchorType.MiddleCenter:
                {
                    rectTransform.SetMiddleCenterAnchor();
                    this.anchorType = Utils.AnchorType.MiddleCenter;
                    return this;
                }
                case Utils.AnchorType.HorizontalStretch:
                {
                    rectTransform.SetHorizontalStretchAnchor();
                    this.anchorType = Utils.AnchorType.HorizontalStretch;
                    return this;
                }
                case Utils.AnchorType.VerticalStretch:
                {
                    rectTransform.SetMiddleCenterAnchor();
                    this.anchorType = Utils.AnchorType.VerticalStretch;
                    return this;
                }
                case Utils.AnchorType.FullStretch:
                {
                    rectTransform.SetFullStretchAnchor();
                    this.anchorType = Utils.AnchorType.FullStretch;
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

        public EGGameObject SetTopLeftAnchor()
        {
            //rectTransform.SetTopLeftAnchor();
            SetAnchorType(Utils.AnchorType.TopLeft);
            return this;
        }

        public EGGameObject SetMiddleCenterAnchor()
        {
            //rectTransform.SetMiddleCenterAnchor();
            SetAnchorType(Utils.AnchorType.MiddleCenter);
            return this;
        }

        public EGGameObject SetHorizontalStretchAnchor()
        {
            //rectTransform.SetHorizontalStretchAnchor();
            SetAnchorType(Utils.AnchorType.HorizontalStretch);
            return this;
        }

        public EGGameObject SetVerticalStretchAnchor()
        {
            // rectTransform.SetVerticalStretchAnchor();
            SetAnchorType(Utils.AnchorType.VerticalStretch);
            return this;
        }

        public EGGameObject SetMiddleLeftAnchor()
        {
            rectTransform.SetMiddleLeftAnchor();
            return this;
        }

        public EGGameObject SetFullStretchAnchor()
        {
            //rectTransform.SetFullStretchAnchor();
            SetAnchorType(Utils.AnchorType.FullStretch);
            return this;
        }

        #endregion
    }
}